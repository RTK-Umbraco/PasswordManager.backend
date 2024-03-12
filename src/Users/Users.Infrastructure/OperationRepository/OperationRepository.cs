using PasswordManager.Users.ApplicationServices.Repositories.Operations;
using PasswordManager.Users.Domain.Operations;
using PasswordManager.Users.Infrastructure.UserRepository;
using PasswordManager.Users.Infrastructure.BaseRepository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace PasswordManager.Users.Infrastructure.OperationRepository;
public class OperationRepository : BaseRepository<Operation, OperationEntity>, IOperationRepository
{
    public OperationRepository(UserContext context) : base(context)
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

    public async Task<ICollection<Operation>> GetUserOperations(Guid userId)
    {
        var user = await GetOperationsDbSet()
                    .Where(x => x.UserId == userId)
                    .AsNoTracking()
                    .ToListAsync();
        return user.Select(Map).ToImmutableHashSet();
    }

    private DbSet<OperationEntity> GetOperationsDbSet()
    {
        if (Context.UsersOperations is null)
            throw new InvalidOperationException("Database configuration not setup correctly");
        return Context.UsersOperations;
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
