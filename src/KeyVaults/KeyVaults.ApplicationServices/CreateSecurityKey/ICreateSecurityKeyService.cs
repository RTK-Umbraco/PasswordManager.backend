using PasswordManager.KeyVaults.Domain.KeyVaults;
using PasswordManager.KeyVaults.Domain.Operations;

namespace PasswordManager.KeyVaults.ApplicationServices.CreateSecurityKey
{
    public interface ICreateSecurityKeyService
    {
        Task<OperationResult> RequestCreateSecurityKey(SecurityKeyModel securityKeyModel, OperationDetails operationDetails);
        Task CreateSecurityKey(SecurityKeyModel securityKeyModel);
    }
}
