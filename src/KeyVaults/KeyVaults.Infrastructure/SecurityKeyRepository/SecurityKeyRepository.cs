using PasswordManager.KeyVaults.ApplicationServices.Repositories.SecurityKey;
using PasswordManager.KeyVaults.Domain.KeyVaults;
using PasswordManager.KeyVaults.Infrastructure.BaseRepository;
using Microsoft.EntityFrameworkCore;

namespace PasswordManager.KeyVaults.Infrastructure.SecurityKeyRepository;
public class SecurityKeyRepository : BaseRepository<SecurityKeyModel, SecurityKeyEntity>, ISecurityKeyRepository
{
    public SecurityKeyRepository(SecurityKeyContext context) : base(context)
    {
    }

    private DbSet<SecurityKeyEntity> GetUserDbSet()
    {
        if (Context.SecurityKeys is null)
            throw new InvalidOperationException("Database configuration not setup correctly");
        return Context.SecurityKeys;
    }

    protected override SecurityKeyModel Map(SecurityKeyEntity entity)
    {
        return SecurityKeyEntityMapper.Map(entity);
    }

    protected override SecurityKeyEntity Map(SecurityKeyModel model)
    {
        return SecurityKeyEntityMapper.Map(model);
    }
}
