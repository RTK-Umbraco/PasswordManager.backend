using PasswordManager.KeyVaults.Domain.Operations;

namespace PasswordManager.KeyVaults.ApplicationServices.KeyVaultManager
{
    public interface IKeyVaultManagerService
    {
        Task<(Guid SecurityKeyId, string ProtectedItem)> RequestProtect(string item, OperationDetails operationDetails);
        Task<string> RequestUnprotect(Guid securityKeyId, string protectedItem, OperationDetails operationDetails);
        Task<string> RequestReprotect(Guid securityKeyId, string protectedItem, OperationDetails operationDetails);
    }
}
