using PasswordManager.Users.ApplicationServices.CreateUserPassword;
using PasswordManager.Users.ApplicationServices.Operations;
using PasswordManager.Users.Domain.Operations;
using Rebus.Bus;
using Rebus.Handlers;
using Users.Messages.CreateUserPassword;

namespace Users.Worker.Service.CreateUserPassword;
public class CreateUserPasswordCommandHandler : IHandleMessages<CreateUserPasswordCommand>
{
    private readonly ICreateUserPasswordService _createUserPasswordService;
    private readonly IOperationService _operationService;
    private readonly ILogger<CreateUserPasswordCommandHandler> _logger;
    private readonly IBus _bus;

    public CreateUserPasswordCommandHandler(ICreateUserPasswordService createUserPasswordService, IOperationService operationService, ILogger<CreateUserPasswordCommandHandler> logger, IBus bus)
    {
        _createUserPasswordService = createUserPasswordService;
        _operationService = operationService;
        _logger = logger;
        _bus = bus;
    }

    public async Task Handle(CreateUserPasswordCommand message)
    {
        var requestId = message.RequestId;
        _logger.LogInformation($"Handling create user password command: {requestId}");

        var operation = await _operationService.GetOperationByRequestId(requestId);

        if (operation is null)
        {
            _logger.LogWarning($"Operation not found: {requestId}");
            throw new InvalidOperationException($"Could not find operation with request id {requestId} when creating user password");
        }

        await _operationService.UpdateOperationStatus(requestId, OperationStatus.Processing);

        var createPasswordModel = CreateUserPasswordOperationHelper.Map(operation.UserId, operation);

        try
        {
            await _createUserPasswordService.CreateUserPassword(createPasswordModel);
            await PublishSuccessEventAndMarkOperationAsCompleted(createPasswordModel.UserId, requestId);

            OperationResult.Completed(operation);
        }
        catch (CreateUserPasswordServiceException exception)
        {
            await PublishFailedEventAndMarkOperationAsFailed(createPasswordModel.UserId, requestId, exception.Message);
            throw;
        }
    }

    private async Task PublishFailedEventAndMarkOperationAsFailed(Guid userId, string requestId, string message)
    {
        await _bus.Publish(new CreateUserPasswordFailedEvent(userId, requestId, message ?? string.Empty));
        await SetOperationStatus(requestId, OperationStatus.Failed);
    }

    private async Task PublishSuccessEventAndMarkOperationAsCompleted(Guid userId, string requestId)
    {
        await _operationService.UpdateOperationStatus(requestId, OperationStatus.Completed);
        await _bus.Publish(new CreateUserPasswordEvent(userId, requestId));
    }

    private async Task SetOperationStatus(string requestId, OperationStatus operationStatus)
    {
        await _operationService.UpdateOperationStatus(requestId, operationStatus);
    }
}
