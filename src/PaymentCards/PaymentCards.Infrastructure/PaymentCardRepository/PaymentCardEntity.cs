using PasswordManager.PaymentCards.Infrastructure.BaseRepository;

namespace PasswordManager.PaymentCards.Infrastructure.PaymentCardRepository;

public class PaymentCardEntity : BaseEntity
{
    public Guid UserId { get; private set; }
    public string CardNumber { get; private set; }
    public string CardHolderName { get; private set; }
    public string ExpiryDate { get; private set; }

    public PaymentCardEntity(Guid id, DateTime createdUtc, DateTime modifiedUtc, bool deleted, Guid userId, string cardNumber, string cardHolderName, string expiryDate) : base(id, createdUtc, modifiedUtc, deleted)
    {
        UserId = userId;
        CardNumber = cardNumber;
        CardHolderName = cardHolderName;
        ExpiryDate = expiryDate;
    }
}