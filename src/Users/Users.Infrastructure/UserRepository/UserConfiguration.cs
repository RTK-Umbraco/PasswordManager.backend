using PasswordManager.Users.Infrastructure.BaseRepository;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PasswordManager.Users.Infrastructure.UserRepository;
public class UserConfiguration : BaseEntityConfiguration<UserEntity>
{
    public override void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.FirebaseId).IsRequired();
    }
}