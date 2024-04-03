using Microsoft.Extensions.Logging;
using PasswordManager.User.Domain.Operations;
using PasswordManager.Users.ApplicationServices.Components;
using PasswordManager.Users.ApplicationServices.Operations;
using PasswordManager.Users.Domain.Operations;
using Rebus.Bus;
using Users.Messages.DeleteUserPassword;

namespace PasswordManager.Users.ApplicationServices.UserPassword.DeleteUserPassword
{
    public class DeleteUserPasswordService : IDeleteUserPasswordService
    {
        private readonly IOperationService _operationService;
        private readonly IPasswordComponent _passwordComponent;
        private readonly ILogger<DeleteUserPasswordService> _logger;
        private readonly IBus _bus;

        public DeleteUserPasswordService(IOperationService operationService, IPasswordComponent passwordComponent, ILogger<DeleteUserPasswordService> logger, IBus bus)
        {
            _operationService = operationService;
            _passwordComponent = passwordComponent;
            _logger = logger;
            _bus = bus;
        }
        public async Task<OperationResult> RequestDeleteUserPassword(Guid passwordId, OperationDetails operationDetails)
        {
            _logger.LogInformation("Request deleting password for user {userId}", passwordId);

            var operation = await _operationService.QueueOperation(OperationBuilder.DeleteUserPassword(passwordId, operationDetails.CreatedBy));

            await _bus.Send(new DeleteUserPasswordCommand(operation.RequestId));

            _logger.LogInformation("Request sent to worker for deleting password: {passwordId} - requestId: {requestId}", passwordId, operation.RequestId);

            return OperationResult.Accepted(operation);
        }

        public async Task DeleteUserPassword(Guid userId, string createdByUserId)
        {
            try
            {
                await _passwordComponent.DeleteUserPassword(userId, createdByUserId);
            }
            catch (PasswordComponentException exception)
            {
                throw new DeleteUserPasswordServiceException($"Error calling password component to delete password for user {userId}", exception);
            }
        }
    }
}
