using PasswordManager.KeyVaults.Infrastructure.BaseRepository;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PasswordManager.KeyVaults.Infrastructure.SecurityKeyRepository;
public class SecurityKeyConfiguration : BaseEntityConfiguration<SecurityKeyEntity>
{
    public override void Configure(EntityTypeBuilder<SecurityKeyEntity> builder)
    {
        base.Configure(builder);
    }
}