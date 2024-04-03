using PasswordManager.Users.ApplicationServices.Operations;
using PasswordManager.Users.ApplicationServices.UserPassword.DeleteUserPassword;
using PasswordManager.Users.Domain.Operations;
using Rebus.Bus;
using Rebus.Handlers;
using Users.Messages.DeleteUserPassword;

namespace Users.Worker.Service.DeleteUserPassword
{
    public class DeleteUserPasswordCommandHandler : IHandleMessages<DeleteUserPasswordCommand>
    {
        private readonly IDeleteUserPasswordService _deleteUserPasswordService;
        private readonly IOperationService _operationService;
        private readonly ILogger<DeleteUserPasswordCommandHandler> _logger;

        public DeleteUserPasswordCommandHandler(IDeleteUserPasswordService deleteUserPasswordService, IOperationService operationService, ILogger<DeleteUserPasswordCommandHandler> logger)
        {
            _deleteUserPasswordService = deleteUserPasswordService;
            _operationService = operationService;
            _logger = logger;
        }

        public async Task Handle(DeleteUserPasswordCommand message)
        {
            _logger.LogInformation("Handling delete user password command: {requestId}", message.RequestId);

            var operation = await _operationService.GetOperationByRequestId(message.RequestId);

            if (operation == null)
            {
                _logger.LogWarning("Operation not found for requestId: {requestId}", message.RequestId);
                return;
            }

            await _operationService.UpdateOperationStatus(operation.RequestId, OperationStatus.Processing);

            try
            {
                await _deleteUserPasswordService.DeleteUserPassword(operation.UserId, operation.CreatedBy);
                await _operationService.UpdateOperationStatus(operation.RequestId, OperationStatus.Completed);
            }
            catch (DeleteUserPasswordServiceException exception)
            {
                _logger.LogError(exception, "Error deleting user password with request id: {requestId}", message.RequestId);

                await _operationService.UpdateOperationStatus(operation.RequestId, OperationStatus.Failed);
            }
        }
    }
}
