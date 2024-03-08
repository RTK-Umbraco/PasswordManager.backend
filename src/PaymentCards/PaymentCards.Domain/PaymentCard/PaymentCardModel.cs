namespace PasswordManager.PaymentCards.Domain.PaymentCards;
public class PaymentCardModel : BaseModel
{
    public PaymentCardModel(Guid id, DateTime createdUtc, DateTime modifiedUtc) : base(id, createdUtc, modifiedUtc)
    {
    }
}
