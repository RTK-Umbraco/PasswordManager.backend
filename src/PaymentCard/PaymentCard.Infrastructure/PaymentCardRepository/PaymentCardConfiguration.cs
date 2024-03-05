using PasswordManager.PaymentCard.Infrastructure.BaseRepository;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PasswordManager.PaymentCard.Infrastructure.PaymentCardRepository;
public class PaymentCardConfiguration : BaseEntityConfiguration<PaymentCardEntity>
{
    public override void Configure(EntityTypeBuilder<PaymentCardEntity> builder)
    {
        base.Configure(builder);
    }
}