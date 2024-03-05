using PasswordManager.Password.Infrastructure.BaseRepository;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PasswordManager.Password.Infrastructure.PasswordRepository;
public class PasswordConfiguration : BaseEntityConfiguration<PasswordEntity>
{
    public override void Configure(EntityTypeBuilder<PasswordEntity> builder)
    {
        base.Configure(builder);
    }
}