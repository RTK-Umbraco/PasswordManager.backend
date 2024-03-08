using Password.Messages.UpdatePassword;
using PasswordManager.Password.ApplicationServices.Operations;
using PasswordManager.Password.ApplicationServices.Repositories.Password;
using PasswordManager.Password.ApplicationServices.UpdatePassword;
using PasswordManager.Password.Domain.Operations;
using Rebus.Bus;
using Rebus.Handlers;

namespace Password.Worker.Service.UpdatePassword;

public class UpdatePasswordCommandHandler : IHandleMessages<UpdatePasswordCommand>
{
    private readonly IUpdatePasswordService _updatePasswordService;
    private readonly IOperationService _operationService;
    private readonly ILogger<UpdatePasswordCommandHandler> _logger;
    private readonly IBus _bus;

    public UpdatePasswordCommandHandler(IUpdatePasswordService updateUserService, IOperationService operationService, ILogger<UpdatePasswordCommandHandler> logger, IBus bus)
    {
        _updatePasswordService = updateUserService;
        _operationService = operationService;
        _logger = logger;
        _bus = bus;
    }

    public async Task Handle(UpdatePasswordCommand message)
    {
        var requestId = message.RequestId;

        var operation = await _operationService.GetOperationByRequestId(requestId);
        if (operation is null)
        {
            _logger.LogError("Could not find operation with request id {RequestId} when updating password", requestId);
            throw new InvalidOperationException($"Could not find operation with request id {requestId} when updating password");
        }

        await _operationService.UpdateOperationStatus(requestId, OperationStatus.Processing);

        //Get new password from operation table in database and maps it to our password domain model
        var updatePasswordModel = UpdatePasswordOperationHelper.Map(operation.PasswordId, operation);

        try
        {
            await _updatePasswordService.UpdatePassword(updatePasswordModel);
        }
        catch (PasswordRepositoryException e)
        {
            await _operationService.UpdateOperationStatus(requestId, OperationStatus.Failed);
            await _bus.Publish(new UpdatePasswordFailedEvent(updatePasswordModel.Id, requestId, e.Message));
            throw;
        }

        //Publish event
        await _operationService.UpdateOperationStatus(requestId, OperationStatus.Completed);
        await _bus.Publish(new UpdatePasswordEvent());

        OperationResult.Completed(operation);
    }
}
