using KeyVaults.Messages.CreateSecurityKey;
using Microsoft.Extensions.Logging;
using PasswordManager.KeyVaults.ApplicationServices.Operations;
using PasswordManager.KeyVaults.ApplicationServices.Repositories.SecurityKey;
using PasswordManager.KeyVaults.Domain.KeyVaults;
using PasswordManager.KeyVaults.Domain.Operations;
using Rebus.Bus;

namespace PasswordManager.KeyVaults.ApplicationServices.CreateSecurityKey
{
    public sealed class CreateSecurityKeyService : ICreateSecurityKeyService
    {
        private readonly ISecurityKeyRepository _securityKeyRepository;
        private readonly IOperationService _operationService;
        private readonly ILogger<CreateSecurityKeyService> _logger;
        private readonly IBus _bus;

        public CreateSecurityKeyService(ISecurityKeyRepository securityKeyRepository, IOperationService operationService, ILogger<CreateSecurityKeyService> logger, IBus bus)
        {
            _securityKeyRepository = securityKeyRepository;
            _operationService = operationService;
            _logger = logger;
            _bus = bus;
        }

        public async Task<OperationResult> RequestCreateSecurityKey(SecurityKeyModel securityKeyModel, OperationDetails operationDetails)
        {
            _logger.LogInformation($"Request creation of SecurityKey {securityKeyModel.Id}");

            // Checks if a security key already exists
            var existingSecurityKey = await _securityKeyRepository.GetById(securityKeyModel.Id);
            if (existingSecurityKey != null)
            {
                _logger.LogInformation($"SecurityKey with ID: {securityKeyModel.Id}, already exists");
                return OperationResult.InvalidState($"SecurityKey with ID: {securityKeyModel.Id}, already exists");
            }

            // Queues the operation
            var operation = await _operationService.QueueOperation(OperationBuilder.CreateSecurityKey(securityKeyModel, operationDetails.CreatedBy));

            // Sends the request to the worker
            await _bus.Send(new CreateSecurityKeyCommand(operation.RequestId));

            _logger.LogInformation($"Request sent to worker for SecurityKey: {securityKeyModel.Id} - requestId: {operation.RequestId}");

            return OperationResult.Accepted(operation);
        }

        public async Task CreateSecurityKey(SecurityKeyModel securityKeyModel)
        {
            _logger.LogInformation($"Creating SecurityKey: {securityKeyModel.Id}");

            await _securityKeyRepository.Upsert(securityKeyModel);

            _logger.LogInformation($"SecurityKey created: {securityKeyModel.Id}");

            return;
        }
    }
}
