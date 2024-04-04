using PasswordManager.Users.ApplicationServices.Operations;
using PasswordManager.Users.ApplicationServices.UserPassword.UpdateUserPassword;
using PasswordManager.Users.Domain.Operations;
using Rebus.Handlers;
using Users.Messages.UpdateUserPassword;

namespace Users.Worker.Service.UpdateUserPassword
{
    public class UpdateUserPasswordCommandHandler : IHandleMessages<UpdateUserPasswordCommand>
    {
        private readonly IUpdateUserPasswordService _updateUserPasswordService;
        private readonly IOperationService _operationService;
        private readonly ILogger<UpdateUserPasswordCommandHandler> _logger;

        public UpdateUserPasswordCommandHandler(IUpdateUserPasswordService updateUserPasswordService, IOperationService operationService, ILogger<UpdateUserPasswordCommandHandler> logger)
        {
            _updateUserPasswordService = updateUserPasswordService;
            _operationService = operationService;
            _logger = logger;
        }

        public async Task Handle(UpdateUserPasswordCommand message)
        {
            _logger.LogInformation("Handling update user password command {requestId}", message.RequestId);

            var operation = await _operationService.GetOperationByRequestId(message.RequestId);

            if (operation == null)
            {
                _logger.LogWarning("Operation not found for requestId {requestId}", message.RequestId);
                return;
            }

            await _operationService.UpdateOperationStatus(operation.RequestId, OperationStatus.Processing);
            var updateUserPasswordModel = UpdateUserPasswordOperationHelper.Map(operation.UserId, operation);

            if (updateUserPasswordModel == null)
            {
                _logger.LogWarning("Could not map operation to update user password model for requestId {requestId}", message.RequestId);
                await _operationService.UpdateOperationStatus(operation.RequestId, OperationStatus.Failed);
                return;
            }

            try
            {
                await _updateUserPasswordService.UpdateUserPassword(updateUserPasswordModel);
                await _operationService.UpdateOperationStatus(operation.RequestId, OperationStatus.Completed);
            }
            catch (UpdateUserPasswordServiceException exception)
            {
                _logger.LogError(exception, "Error updating user password with request id: {requestId}", message.RequestId);
                await _operationService.UpdateOperationStatus(operation.RequestId, OperationStatus.Failed);
                return;
            }
        }
    }
}
