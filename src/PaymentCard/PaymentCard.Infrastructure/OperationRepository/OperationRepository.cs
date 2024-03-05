using PasswordManager.PaymentCard.ApplicationServices.Repositories.Operations;
using PasswordManager.PaymentCard.Domain.Operations;
using PasswordManager.PaymentCard.Infrastructure.PaymentCardRepository;
using PasswordManager.PaymentCard.Infrastructure.BaseRepository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace PasswordManager.PaymentCard.Infrastructure.OperationRepository;
public class OperationRepository : BaseRepository<Operation, OperationEntity>, IOperationRepository
{
    public OperationRepository(PaymentCardContext context) : base(context)
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

    public async Task<ICollection<Operation>> GetPaymentCardOperations(Guid paymentcardId)
    {
        var paymentcard = await GetOperationsDbSet()
                    .Where(x => x.PaymentCardId == paymentcardId)
                    .AsNoTracking()
                    .ToListAsync();
        return paymentcard.Select(Map).ToImmutableHashSet();
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
