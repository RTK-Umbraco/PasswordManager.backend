using PasswordManager.KeyVaults.Domain.Operations;

namespace PasswordManager.KeyVaults.ApplicationServices.KeyVaultManager
{
    public interface IKeyVaultManagerService
    {
        Task<(Guid SecurityKeyId, string ProtectedText)> RequestProtect(string plainText, OperationDetails operationDetails);
        Task<string> RequestUnprotect(Guid securityKeyId, string protectedText, OperationDetails operationDetails);
        Task<string> RequestReprotect(Guid securityKeyId, string protectedText, OperationDetails operationDetails);
    }
}
