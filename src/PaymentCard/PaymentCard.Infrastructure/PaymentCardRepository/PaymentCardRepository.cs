using PasswordManager.PaymentCard.ApplicationServices.Repositories.PaymentCard;
using PasswordManager.PaymentCard.Domain.PaymentCard;
using PasswordManager.PaymentCard.Infrastructure.BaseRepository;
using Microsoft.EntityFrameworkCore;

namespace PasswordManager.PaymentCard.Infrastructure.PaymentCardRepository;
public class PaymentCardRepository : BaseRepository<PaymentCardModel, PaymentCardEntity>, IPaymentCardRepository
{
    public PaymentCardRepository(PaymentCardContext context) : base(context)
    {
    }

    private DbSet<PaymentCardEntity> GetUserDbSet()
    {
        if (Context.PaymentCards is null)
            throw new InvalidOperationException("Database configuration not setup correctly");
        return Context.PaymentCards;
    }

    protected override PaymentCardModel Map(PaymentCardEntity entity)
    {
        return PaymentCardEntityMapper.Map(entity);
    }

    protected override PaymentCardEntity Map(PaymentCardModel model)
    {
        return PaymentCardEntityMapper.Map(model);
    }
}
