using PasswordManager.KeyVaults.ApplicationServices.Repositories.Operations;
using PasswordManager.KeyVaults.Domain.Operations;
using PasswordManager.KeyVaults.Infrastructure.SecurityKeyRepository;
using PasswordManager.KeyVaults.Infrastructure.BaseRepository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace PasswordManager.KeyVaults.Infrastructure.OperationRepository;
public class OperationRepository : BaseRepository<Operation, OperationEntity, SecurityKeyContext>, IOperationRepository
{
    public OperationRepository(SecurityKeyContext context) : base(context)
    {
        MapEntityToModel = OperationMapper.Map;
        MapModelToEntity = OperationMapper.Map;
    }

    public async Task<Operation?> GetByRequestId(string requestId)
    {
        var operationEntity = await Context.Operations
            .Where(x => x.RequestId == requestId)
            .AsNoTracking()
            .SingleOrDefaultAsync();
        return operationEntity is null ? null : MapEntityToModel(operationEntity);
    }

    public async Task<ICollection<Operation>> GetSecurityKeyOperations(Guid securitykeyId)
    {
        var securitykey = await Context.Operations
                    .Where(x => x.SecurityKeyId == securitykeyId)
                    .AsNoTracking()
                    .ToListAsync();
        return securitykey.Select(MapEntityToModel).ToImmutableHashSet();
    }
}