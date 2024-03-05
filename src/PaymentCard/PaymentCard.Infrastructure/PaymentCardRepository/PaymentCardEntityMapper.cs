using PasswordManager.PaymentCard.Domain.PaymentCard;

namespace PasswordManager.PaymentCard.Infrastructure.PaymentCardRepository;
internal static class PaymentCardEntityMapper
{
    internal static PaymentCardEntity Map(PaymentCardModel model)
    {
        return new PaymentCardEntity(
            model.Id,
            model.CreatedUtc,
            model.ModifiedUtc
            );
    }

    internal static PaymentCardModel Map(PaymentCardEntity entity)
    {
        return new PaymentCardModel(
            entity.Id,
            entity.CreatedUtc,
            entity.ModifiedUtc
            );
    }
}
