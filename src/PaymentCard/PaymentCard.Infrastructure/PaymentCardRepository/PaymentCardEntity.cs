using PasswordManager.PaymentCard.Infrastructure.BaseRepository;

namespace PasswordManager.PaymentCard.Infrastructure.PaymentCardRepository;

public class PaymentCardEntity : BaseEntity
{
    public PaymentCardEntity(Guid id, DateTime createdUtc, DateTime modifiedUtc) : base(id, createdUtc, modifiedUtc)
    {
    }
}