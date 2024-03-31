using Microsoft.Extensions.Logging;
using Password.Messages.DeletePassword;
using PasswordManager.Password.ApplicationServices.Operations;
using PasswordManager.Password.ApplicationServices.Repositories.Password;
using PasswordManager.Password.Domain.Operations;
using PasswordManager.Password.Domain.Password;
using Rebus.Bus;

namespace PasswordManager.Password.ApplicationServices.Password.DeletePassword
{
    public sealed class DeletePasswordService : IDeletePasswordService
    {
        private readonly IOperationService _operationService;
        private readonly IBus _bus;
        private readonly IPasswordRepository _passwordRepository;
        private readonly ILogger<DeletePasswordService> _logger;

        public DeletePasswordService(IPasswordRepository passwordRepository)
        {
            _passwordRepository = passwordRepository;
        }

        public async Task<OperationResult> RequestDeletePassword(Guid passwordId, OperationDetails operationDetails)
        {
            _logger.LogInformation($"Request deletion of password with id {passwordId}");

            var password = await _passwordRepository.Get(passwordId);

            if (password == null)
            {
                _logger.LogWarning($"Could not delete password: {passwordId}, not found");
                return OperationResult.InvalidState($"Password could not be deleted as it was not found");
            }

            if (password.Deleted)
            {
                _logger.LogWarning($"Password is already marked deleted: {passwordId}");
                return OperationResult.InvalidState($"Cannot delete a password that is marked deleted");
            }

            var operation = await _operationService.QueueOperation(OperationBuilder.DeletePassword(password, operationDetails.CreatedBy));

            await _bus.Send(new DeletePasswordCommand(operation.RequestId));

            _logger.LogInformation($"Request sent to worker for deletion of password: {passwordId}");

            return OperationResult.Accepted(operation);
        }

        public async Task DeletePassword(PasswordModel deletePasswordModel)
        {
            _logger.LogInformation($"Deleting password with id {deletePasswordModel.Id}");

            await _passwordRepository.Delete(deletePasswordModel.Id);

            _logger.LogInformation($"Password with id {deletePasswordModel.Id} deleted");

            return;
        }
    }
}
