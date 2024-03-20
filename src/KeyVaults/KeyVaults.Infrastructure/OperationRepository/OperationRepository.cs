using PasswordManager.KeyVaults.ApplicationServices.Repositories.Operations;
using PasswordManager.KeyVaults.Domain.Operations;
using PasswordManager.KeyVaults.Infrastructure.SecurityKeyRepository;
using PasswordManager.KeyVaults.Infrastructure.BaseRepository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace PasswordManager.KeyVaults.Infrastructure.OperationRepository;
public class OperationRepository : BaseRepository<Operation, OperationEntity>, IOperationRepository
{
    public OperationRepository(SecurityKeyContext context) : base(context)
    {
    }

    public async Task<Operation?> GetByRequestId(string requestId)
    {
        var operationEntity = await GetOperationsDbSet()
            .Where(x => x.RequestId == requestId)
            .AsNoTracking()
            .SingleOrDefaultAsync();
        return operationEntity is null ? null : Map(operationEntity);
    }

    public async Task<ICollection<Operation>> GetSecurityKeyOperations(Guid securityKeyid)
    {
        var user = await GetOperationsDbSet()
                    .Where(x => x.SecurityKeyId == securityKeyid)
                    .AsNoTracking()
                    .ToListAsync();
        return user.Select(Map).ToImmutableHashSet();
    }

    private DbSet<OperationEntity> GetOperationsDbSet()
    {
        if (Context.Operations is null)
            throw new InvalidOperationException("Database configuration not setup correctly");
        return Context.Operations;
    }

    protected override Operation Map(OperationEntity entity)
    {
        return OperationMapper.Map(entity);
    }

    protected override OperationEntity Map(Operation model)
    {
        return OperationMapper.Map(model);
    }
}