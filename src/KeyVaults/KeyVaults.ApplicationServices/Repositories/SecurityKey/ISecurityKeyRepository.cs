using PasswordManager.KeyVaults.Domain.KeyVaults;

namespace PasswordManager.KeyVaults.ApplicationServices.Repositories.SecurityKey;
public interface ISecurityKeyRepository : IBaseRepository<SecurityKeyModel>
{
    Task<SecurityKeyModel?> GetSecurityKeyByObjectId(Guid objectId);
}
