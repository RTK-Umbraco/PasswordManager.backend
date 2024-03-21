namespace PasswordManager.PaymentCards.Domain.PaymentCards;
public class PaymentCardModel : BaseModel
{
    public Guid UserId { get; }
    public string CardNumber { get; private set; }
    public string CardHolderName { get; private set; }
    public string ExpiryDate { get; private set; }

    public PaymentCardModel(Guid id, Guid userId, string cardNumber, string cardHolderName, string expiryDate) : base(id)
    {
        UserId = userId;
        CardNumber = cardNumber;
        CardHolderName = cardHolderName;
        ExpiryDate = expiryDate;
    }

    public PaymentCardModel(Guid id, string cardNumber, string cardHolderName, string expiryDate) : base(id)
    {
        CardNumber = cardNumber;
        CardHolderName = cardHolderName;
        ExpiryDate = expiryDate;
    }

    public PaymentCardModel(Guid id, DateTime createdUtc, DateTime modifiedUtc, bool deleted, Guid userId, string cardNumber, string cardHolderName, string expiryDate) : base(id, createdUtc, modifiedUtc, deleted)
    {
        UserId = userId;
        CardNumber = cardNumber;
        CardHolderName = cardHolderName;
        ExpiryDate = expiryDate;
    }

    public PaymentCardModel Create(Guid userId, string cardNumber, string cardHolderName, string expiryDate)
    {
        return new PaymentCardModel(userId, Guid.NewGuid(), cardNumber, cardHolderName, expiryDate);
    }

    public PaymentCardModel Update(Guid id, string cardNumber, string cardHolderName, string expiryDate)
    {
        return new PaymentCardModel(id, cardNumber, cardHolderName, expiryDate);
    }
}
