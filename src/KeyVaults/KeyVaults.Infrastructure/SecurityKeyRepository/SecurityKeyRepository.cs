using PasswordManager.KeyVaults.ApplicationServices.Repositories.SecurityKey;
using PasswordManager.KeyVaults.Domain.KeyVaults;
using PasswordManager.KeyVaults.Infrastructure.BaseRepository;

namespace PasswordManager.KeyVaults.Infrastructure.SecurityKeyRepository;
public class SecurityKeyRepository : BaseRepository<SecurityKeyModel, SecurityKeyEntity>, ISecurityKeyRepository
{
    public SecurityKeyRepository(SecurityKeyContext context) : base(context)
    {
    }

    protected override SecurityKeyModel Map(SecurityKeyEntity entity)
    {
        throw new NotImplementedException();
    }

    protected override SecurityKeyEntity Map(SecurityKeyModel model)
    {
        throw new NotImplementedException();
    }
}
