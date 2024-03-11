using PasswordManager.Password.Infrastructure.BaseRepository;

namespace PasswordManager.Password.Infrastructure.PasswordRepository;
public class PasswordConfiguration : BaseEntityConfiguration<PasswordEntity>
{
    public override void Configure(EntityTypeBuilder<PasswordEntity> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.FriendlyName).IsRequired();
        builder.Property(p => p.Url).IsRequired();
        builder.Property(p => p.Password).IsRequired();
        builder.Property(p => p.Username).IsRequired();
        builder.Property(p => p.Deleted).IsRequired();
    }
}