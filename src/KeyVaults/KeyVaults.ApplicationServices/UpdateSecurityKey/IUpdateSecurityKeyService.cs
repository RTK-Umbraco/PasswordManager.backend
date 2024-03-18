using PasswordManager.KeyVaults.Domain.Operations;
using PasswordManager.KeyVaults.Domain.KeyVaults;


namespace PasswordManager.KeyVaults.ApplicationServices.UpdateSecurityKey
{
    public interface IUpdateSecurityKeyService
    {
        Task<OperationResult> RequestUpdateSecurityKey(SecurityKeyModel securityKey, OperationDetails operationDetails);
        Task UpdateSecurityKey(SecurityKeyModel securityKey);
    }
}