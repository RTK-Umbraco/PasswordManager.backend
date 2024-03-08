namespace PasswordManager.PaymentCards.ApplicationServices.Repositories.PaymentCard;
internal class PaymentCardException : Exception
{
    public PaymentCardException(string? message) : base(message) { }
}
