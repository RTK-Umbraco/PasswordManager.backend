using Microsoft.Extensions.Logging;
using PasswordManager.User.Domain.Operations;
using PasswordManager.Users.ApplicationServices.Components;
using PasswordManager.Users.ApplicationServices.Operations;
using PasswordManager.Users.ApplicationServices.Repositories.User;
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
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UpdateUserPasswordService> _logger;
        private readonly IBus _bus;

        public UpdateUserPasswordService(
            IPasswordComponent passwordComponent, 
            IOperationService operationService, 
            IKeyVaultComponent keyVaultComponent, 
            ILogger<UpdateUserPasswordService> logger, 
            IBus bus, IUserRepository userRepository)
        {
            _passwordComponent = passwordComponent;
            _operationService = operationService;
            _keyVaultComponent = keyVaultComponent;
            _userRepository = userRepository;
            _logger = logger;
            _bus = bus;
        }

        public async Task<OperationResult> RequestUpdateUserPassword(UserPasswordModel userPasswordModel, OperationDetails operationDetails)
        {
            _logger.LogInformation("Request updating password for user {userId}", userPasswordModel.UserId);

            var user = await _userRepository.Get(userPasswordModel.UserId);

            if (user is null)
            {
                return OperationResult.InvalidState("Cannot update password for user because user was not found");
            }

            if (user.IsDeleted())
            {
                return OperationResult.InvalidState("Cannot update user password because user was marked as deleted");
            }

            try
            {
                var encryptedPassword = await _keyVaultComponent.CreateEncryptedPassword(userPasswordModel, user.SecretKey);

                if (string.IsNullOrEmpty(encryptedPassword))
                {
                    return OperationResult.InvalidState("Cannot create encrypted password for user");
                }

                userPasswordModel.SetEncryptedPassword(encryptedPassword);

                var operation = await _operationService.QueueOperation(OperationBuilder.UpdateUserPassword(userPasswordModel, operationDetails.CreatedBy));

                await _bus.Send(new UpdateUserPasswordCommand(operation.RequestId));

                _logger.LogInformation("Request sent to worker for updating password: {userId} - requestId: {requestId}", userPasswordModel.UserId, operation.RequestId);

                return OperationResult.Accepted(operation);
            }
            catch (UpdateUserPasswordServiceException exception)
            {
                return OperationResult.InvalidState($"Error updating password for user {userPasswordModel.UserId}. Exception: {exception}");
            }
        }

        public async Task UpdateUserPassword(UserPasswordModel userPasswordModel)
        {
            _logger.LogInformation("Updating password {passwordId} for user {userId}", userPasswordModel.PasswordId, userPasswordModel.UserId);
            try
            {
                await _passwordComponent.UpdateUserPassword(userPasswordModel);
                _logger.LogInformation("Password {passwordId} updated for user {userId}", userPasswordModel.PasswordId, userPasswordModel.UserId);
            }
            catch (PasswordComponentException exception)
            {
                throw new UpdateUserPasswordServiceException($"Error calling password component to update password for user {userPasswordModel.UserId}", exception);
            }
        }
    }
}
