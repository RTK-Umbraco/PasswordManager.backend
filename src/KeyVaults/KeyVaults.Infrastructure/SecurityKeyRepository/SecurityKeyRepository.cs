using PasswordManager.KeyVaults.ApplicationServices.Repositories.SecurityKey;
using PasswordManager.KeyVaults.Domain.KeyVaults;
using PasswordManager.KeyVaults.Infrastructure.BaseRepository;
using Microsoft.EntityFrameworkCore;

namespace PasswordManager.KeyVaults.Infrastructure.SecurityKeyRepository;
public class SecurityKeyRepository : BaseRepository<SecurityKeyModel, SecurityKeyEntity, SecurityKeyContext>, ISecurityKeyRepository
{
    public SecurityKeyRepository(SecurityKeyContext context) : base(context)
    {
        MapEntityToModel = SecurityKeyEntityMapper.Map;
        MapModelToEntity = SecurityKeyEntityMapper.Map;
    }

    public async Task<SecurityKeyModel?> GetSecurityKeyByObjectId(Guid objectId) =>
        await Context.SecurityKeys
        .AsNoTracking()
        .Where(x => x.ObjectId == objectId && !x.Deleted)
        .Select(x => MapEntityToModel(x))
        .FirstOrDefaultAsync();
}
