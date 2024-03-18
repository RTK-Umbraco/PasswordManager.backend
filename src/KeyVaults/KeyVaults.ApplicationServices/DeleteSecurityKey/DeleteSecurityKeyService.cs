﻿using Microsoft.Extensions.Logging;
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

        public async Task<OperationResult> RequestDeleteSecurityKey(Guid objectId, OperationDetails operationDetails)
        {
            _logger.LogInformation($"Request deletion of security key with objectId: {objectId}");

            var securityKey = await _securityKeyRepository.GetSecurityKeyByObjectId(objectId);

            if (securityKey == null)
            {
                _logger.LogWarning($"Security key with objectId: {objectId} not found");
                return OperationResult.InvalidState("Security key does not exist or is already deleted");
            }

            var operation = await _operationService.QueueOperation(OperationBuilder.DeleteSecurityKey(securityKey, operationDetails.CreatedBy));

            await _bus.Send(new DeleteSecurityKeyCommand(operation.RequestId));

            _logger.LogInformation($"Request sent to worker for deleting security key: {securityKey.Id} - requestId: {operation.RequestId}");

            return OperationResult.Accepted(operation);
        }

        public Task DeleteSecurityKey(Guid securityKeyId)
        {
            _logger.LogInformation($"Deleting security key: {securityKeyId}");

            return _securityKeyRepository.Delete(securityKeyId);
        }
    }
}