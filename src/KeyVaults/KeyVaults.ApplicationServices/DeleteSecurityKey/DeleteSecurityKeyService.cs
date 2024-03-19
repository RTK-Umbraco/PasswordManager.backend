using Microsoft.Extensions.Logging;
using PasswordManager.KeyVaults.ApplicationServices.CreateSecurityKey;
using PasswordManager.KeyVaults.ApplicationServices.Repositories.SecurityKey;
using PasswordManager.KeyVaults.ApplicationServices.Operations;
using Rebus.Bus;
using PasswordManager.KeyVaults.Domain.Operations;
using KeyVaults.Messages.DeleteSecurityKey;

namespace PasswordManager.KeyVaults.ApplicationServices.DeleteSecurityKey
{
    public sealed class DeleteSecurityKeyService : IDeleteSecurityKeyService
    {
        private readonly ISecurityKeyRepository _securityKeyRepository;
        private readonly IOperationService _operationService;
        private readonly ILogger<CreateSecurityKeyService> _logger;
        private readonly IBus _bus;

        public DeleteSecurityKeyService(ISecurityKeyRepository securityKeyRepository, IOperationService operationService, ILogger<CreateSecurityKeyService> logger, IBus bus)
        {
            _securityKeyRepository = securityKeyRepository;
            _operationService = operationService;
            _logger = logger;
            _bus = bus;
        }

        public async Task<OperationResult> RequestDeleteSecurityKey(Guid securityKeyId, OperationDetails operationDetails)
        {
            _logger.LogInformation($"Request deletion of SecurityKey with ID: {securityKeyId}");

            var securityKey = await _securityKeyRepository.GetById(securityKeyId);

            if (securityKey == null)
            {
                _logger.LogWarning($"SecurityKey with ID: {securityKeyId}, not found");
                return OperationResult.InvalidState("Security key does not exist or is already deleted");
            }

            var operation = await _operationService.QueueOperation(OperationBuilder.DeleteSecurityKey(securityKey, operationDetails.CreatedBy));

            await _bus.Send(new DeleteSecurityKeyCommand(operation.RequestId));

            _logger.LogInformation($"Request sent to worker for deleting SecurityKey: {securityKey.Id} - requestId: {operation.RequestId}");

            return OperationResult.Accepted(operation);
        }

        public Task DeleteSecurityKey(Guid securityKeyId)
        {
            _logger.LogInformation($"Deleting SecurityKey: {securityKeyId}");

            return _securityKeyRepository.Delete(securityKeyId);
        }
    }
}
