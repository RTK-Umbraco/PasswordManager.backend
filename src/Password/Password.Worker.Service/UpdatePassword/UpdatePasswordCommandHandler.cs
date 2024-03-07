using Password.Messages.UpdatePassword;
using Password.Worker.Service.CreatePassword;
using PasswordManager.Password.ApplicationServices.Operations;
using PasswordManager.Password.ApplicationServices.Repositories.Password;
using PasswordManager.Password.ApplicationServices.UpdatePassword;
using PasswordManager.Password.Domain.Operations;
using Rebus.Handlers;

namespace Password.Worker.Service.UpdatePassword;

public class UpdatePasswordCommandHandler : IHandleMessages<UpdatePasswordCommand>
{
    private readonly IUpdatePasswordService _updatePasswordService;
    private readonly IOperationService _operationService;
    private readonly ILogger<UpdatePasswordCommandHandler> _logger;

    public UpdatePasswordCommandHandler(IUpdatePasswordService updateUserService, IOperationService operationService, ILogger<UpdatePasswordCommandHandler> logger)
    {
        _updatePasswordService = updateUserService;
        _operationService = operationService;
        _logger = logger;
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

        var updatePasswordModel = UpdatePasswordOperationHelper.Map(operation.PasswordId, operation);

        try
        {
            await _updatePasswordService.UpdatePassword(updatePasswordModel);
        }
        catch (PasswordRepositoryException)
        {
            await _operationService.UpdateOperationStatus(requestId, OperationStatus.Failed);
            throw;
        }

        await _operationService.UpdateOperationStatus(requestId, OperationStatus.Completed);

        OperationResult.Completed(operation);
    }
}
