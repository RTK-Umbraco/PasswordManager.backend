using Microsoft.Extensions.Logging;
using PasswordManager.KeyVaults.ApplicationServices.CreateSecurityKey;
using PasswordManager.KeyVaults.ApplicationServices.Protection;
using PasswordManager.KeyVaults.ApplicationServices.Repositories.SecurityKey;
using PasswordManager.KeyVaults.ApplicationServices.UpdateSecurityKey;
using PasswordManager.KeyVaults.Domain.KeyVaults;
using PasswordManager.KeyVaults.Domain.Operations;

namespace PasswordManager.KeyVaults.ApplicationServices.KeyVaultManager
{
    public class KeyVaultManagerService : IKeyVaultManagerService
    {
        private readonly ICreateSecurityKeyService _createSecurityKeyService;
        private readonly IUpdateSecurityKeyService _updateSecurityKeyService;
        private readonly IProtectionService _protectionService;
        private readonly ISecurityKeyRepository _securityKeyRepository;
        private readonly ILogger<KeyVaultManagerService> _logger;

        public KeyVaultManagerService(ICreateSecurityKeyService createSecurityKeyService, IUpdateSecurityKeyService updateSecurityKeyService, IProtectionService protectionService, ISecurityKeyRepository securityKeyRepository, ILogger<KeyVaultManagerService> logger)
        {
            _createSecurityKeyService = createSecurityKeyService;
            _updateSecurityKeyService = updateSecurityKeyService;
            _protectionService = protectionService;
            _securityKeyRepository = securityKeyRepository;
            _logger = logger;
        }

        public async Task<(Guid SecurityKeyId, string ProtectedText)> RequestProtect(string plainText, OperationDetails operationDetails)
        {
            // Creates new security key object
            var newSecurityKey = SecurityKeyModel.Create(_protectionService.GenerateSecretKey(128));
            
            _logger.LogInformation($"Request protection with SecurityKeyId: {newSecurityKey.Id}");

            // Protecting text
            string protectedText = _protectionService.Protect(plainText, newSecurityKey.SecretKey);

            // Saves the new security key to database
            var operation = await _createSecurityKeyService.RequestCreateSecurityKey(newSecurityKey, operationDetails);

            return operation.Status switch
            {
                OperationResultStatus.Accepted => (SecurityKeyId: newSecurityKey.Id, ProtectedText: protectedText),
                OperationResultStatus.InvalidOperationRequest =>
                throw new KeyVaultManagerServiceException($"ERROR: Failed to create SecurityKey"),
                _ => throw new KeyVaultManagerServiceException("ERROR: Unknown")
            };
        }

        public async Task<string> RequestReprotect(Guid securityKeyId, string protectedText, OperationDetails operationDetails)
        {
            _logger.LogInformation($"Request reprotection for SecurityKey: {securityKeyId}");

            // Get the security key from the database
            var securityKey = await _securityKeyRepository.GetById(securityKeyId);

            if (securityKey == null)
            {
                _logger.LogInformation($"ERROR: SecurityKey with ID: {securityKeyId}, not found");
                throw new KeyVaultManagerServiceException($"ERROR: SecurityKey with ID: {securityKeyId}, not found");
            }

            // Unprotects the text
            string plainText = _protectionService.Unprotect(protectedText, securityKey.SecretKey);

            // Generates new security key
            securityKey.UpdateSecretKey(_protectionService.GenerateSecretKey(128));

            // Protects the text with the new security key
            string newProtectedText = _protectionService.Protect(plainText, securityKey.SecretKey);

            // Updates the security key in the database
            var operation = await _updateSecurityKeyService.RequestUpdateSecurityKey(securityKey, operationDetails);

            return operation.Status switch
            {
                OperationResultStatus.Accepted => newProtectedText,
                OperationResultStatus.InvalidOperationRequest =>
                throw new KeyVaultManagerServiceException($"ERROR: Failed to update SecurityKey"),
                _ => throw new KeyVaultManagerServiceException("ERROR: Unknown")
            };
        }

        public async Task<string> RequestUnprotect(Guid securityKeyId, string protectedText, OperationDetails operationDetails)
        {
            _logger.LogInformation($"Request unprotection for object: {securityKeyId}");

            // Get the security key from the database
            var securityKey = await _securityKeyRepository.GetById(securityKeyId);

            if (securityKey == null)
            {
                _logger.LogInformation($"ERROR: SecurityKey with ID: {securityKeyId}, not found");
                throw new KeyVaultManagerServiceException($"ERROR: SecurityKey with ID: {securityKeyId}, not found");
            }

            // Unprotects the text
            string plainText = _protectionService.Unprotect(protectedText, securityKey.SecretKey);

            return plainText;
        }
    }
}
