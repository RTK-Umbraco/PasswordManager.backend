using Microsoft.Extensions.Logging;
using PasswordManager.KeyVaults.ApplicationServices.CreateSecurityKey;
using PasswordManager.KeyVaults.ApplicationServices.Repositories.SecurityKey;
using PasswordManager.KeyVaults.ApplicationServices.Operations;
using Rebus.Bus;
using PasswordManager.KeyVaults.Domain.Operations;
using PasswordManager.KeyVaults.Domain.KeyVaults;
using KeyVaults.Messages.UpdateSecurityKey;

namespace PasswordManager.KeyVaults.ApplicationServices.UpdateSecurityKey
{
    public sealed class UpdateSecurityKeyService : IUpdateSecurityKeyService
    {
        private readonly ISecurityKeyRepository _securityKeyRepository;
        private readonly IOperationService _operationService;
        private readonly ILogger<CreateSecurityKeyService> _logger;
        private readonly IBus _bus;

        public UpdateSecurityKeyService(ISecurityKeyRepository securityKeyRepository, IOperationService operationService, ILogger<CreateSecurityKeyService> logger, IBus bus)
        {
            _securityKeyRepository = securityKeyRepository;
            _operationService = operationService;
            _logger = logger;
            _bus = bus;
        }

        public async Task<OperationResult> RequestUpdateSecurityKey(SecurityKeyModel securityKeyModel, OperationDetails operationDetails)
        {
            _logger.LogInformation($"Request update for security key {securityKeyModel.Id}");

            // Check if the security key exists
            var existingSecurityKey = await _securityKeyRepository.GetById(securityKeyModel.Id);
            if (existingSecurityKey == null)
            {
                _logger.LogInformation($"SecurityKey with id: {securityKeyModel.Id} does not exist");
                return OperationResult.InvalidState($"SecurityKey with id: {securityKeyModel.Id} does not exist");
            }

            var operation = await _operationService.QueueOperation(OperationBuilder.UpdateSecurityKey(securityKeyModel, operationDetails.CreatedBy));

            await _bus.Send(new UpdateSecurityKeyCommand(operation.RequestId));

            _logger.LogInformation($"Request sent to worker for updating SecurityKey with id: {securityKeyModel.Id} - requestId: {operation.RequestId}");

            return OperationResult.Accepted(operation);
        }

        public async Task UpdateSecurityKey(SecurityKeyModel securityKeyModel)
        {
            _logger.LogInformation($"Updating SecurityKey: {securityKeyModel.Id}");

            await _securityKeyRepository.Upsert(securityKeyModel);

            _logger.LogInformation($"SecurityKey updated: {securityKeyModel.Id}");
            return;
        }
    }
}
