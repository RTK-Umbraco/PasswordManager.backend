using PasswordManager.KeyVaults.ApplicationServices.Repositories.SecurityKey;
using PasswordManager.KeyVaults.Domain.KeyVaults;
using PasswordManager.KeyVaults.Infrastructure.BaseRepository;
using Microsoft.EntityFrameworkCore;

namespace PasswordManager.KeyVaults.Infrastructure.SecurityKeyRepository;
public class SecurityKeyRepository : BaseRepository<SecurityKeyModel, SecurityKeyEntity, SecurityKeyContext>, ISecurityKeyRepository
{
    public SecurityKeyRepository(SecurityKeyContext context) : base(context, SecurityKeyEntityMapper.Map, SecurityKeyEntityMapper.Map)
    {
    }
}
