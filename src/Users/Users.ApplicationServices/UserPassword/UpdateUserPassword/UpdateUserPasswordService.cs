using Microsoft.Extensions.Logging;
using PasswordManager.User.Domain.Operations;
using PasswordManager.Users.ApplicationServices.Components;
using PasswordManager.Users.ApplicationServices.Operations;
using PasswordManager.Users.Domain.Operations;
using PasswordManager.Users.Domain.User;
using Rebus.Bus;
using Users.Messages.UpdateUserPassword;

namespace PasswordManager.Users.ApplicationServices.UserPassword.UpdateUserPassword
{
    public class UpdateUserPasswordService : IUpdateUserPasswordService
    {
        private readonly IPasswordComponent _passwordComponent;
        private readonly IOperationService _operationService;
        private readonly IKeyVaultComponent _keyVaultComponent;
        private readonly ILogger _logger;
        private readonly IBus _bus;

        public UpdateUserPasswordService(IPasswordComponent passwordComponent, IOperationService operationService, IKeyVaultComponent keyVaultComponent, ILogger logger, IBus bus)
        {
            _passwordComponent = passwordComponent;
            _operationService = operationService;
            _keyVaultComponent = keyVaultComponent;
            _logger = logger;
            _bus = bus;
        }

        public async Task<OperationResult> RequestUpdateUserPassword(UserPasswordModel userPasswordModel, OperationDetails operationDetails)
        {
            _logger.LogInformation("Request updating password for user {userId}", userPasswordModel.UserId);

            var operation = await _operationService.QueueOperation(OperationBuilder.UpdateUserPassword(userPasswordModel, operationDetails.CreatedBy));

            await _bus.Send(new UpdateUserPasswordCommand(operation.RequestId));

            _logger.LogInformation("Request sent to worker for updating password: {userId} - requestId: {requestId}", userPasswordModel.UserId, operation.RequestId);

            return OperationResult.Accepted(operation);
        }

        public async Task UpdateUserPassword(UserPasswordModel userPasswordModel, string createdByUserId)
        {
            _logger.LogInformation("Updating password {passwordId} for user {userId}", userPasswordModel.PasswordId, userPasswordModel.UserId);
            try
            {
                await _passwordComponent.UpdateUserPassword(userPasswordModel, createdByUserId);
                _logger.LogInformation("Password {passwordId} updated for user {userId}", userPasswordModel.PasswordId, userPasswordModel.UserId);
            }
            catch (PasswordComponentException exception)
            {
                throw new UpdateUserPasswordServiceException($"Error calling password component to update password for user {userPasswordModel.UserId}", exception);
            }
        }
    }
}
