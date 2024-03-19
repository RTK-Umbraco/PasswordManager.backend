using PasswordManager.KeyVaults.Domain.Operations;

namespace PasswordManager.KeyVaults.ApplicationServices.DeleteSecurityKey
{
    public interface IDeleteSecurityKeyService
    {
        Task<OperationResult> RequestDeleteSecurityKey(Guid securityKeyId, OperationDetails operationDetails);
        Task DeleteSecurityKey(Guid securityKeyId);
    }
}
