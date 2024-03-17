using PasswordManager.KeyVaults.Domain.Operations;

namespace PasswordManager.KeyVaults.ApplicationServices.KeyVaultManager
{
    public interface IKeyVaultManagerService
    {
        Task<string> RequestProtect(Guid objectId, string plainText, OperationDetails operationDetails);
        Task<string> RequestUnprotect(Guid objectId, string protectedText, OperationDetails operationDetails);
        Task<string> RequestReprotect(Guid objectId, string protectedText, OperationDetails operationDetails);
    }
}
