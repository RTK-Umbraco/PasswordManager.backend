using PasswordManager.PaymentCards.Infrastructure.BaseRepository;

namespace PasswordManager.PaymentCards.Infrastructure.PaymentCardRepository;

public class PaymentCardEntity : BaseEntity
{
    public PaymentCardEntity(Guid id, DateTime createdUtc, DateTime modifiedUtc) : base(id, createdUtc, modifiedUtc)
    {
    }
}