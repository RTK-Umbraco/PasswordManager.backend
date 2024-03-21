using PasswordManager.PaymentCards.Infrastructure.BaseRepository;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PasswordManager.PaymentCards.Infrastructure.PaymentCardRepository;
public class PaymentCardConfiguration : BaseEntityConfiguration<PaymentCardEntity>
{
    public override void Configure(EntityTypeBuilder<PaymentCardEntity> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.UserId).IsRequired();
        builder.Property(x => x.CardNumber).IsRequired();
        builder.Property(x => x.CardHolderName).IsRequired();
        builder.Property(x => x.ExpiryDate).IsRequired();
    }
}