using PasswordManager.KeyVaults.Infrastructure.BaseRepository;

namespace PasswordManager.KeyVaults.Infrastructure.SecurityKeyRepository;

public class SecurityKeyEntity : BaseEntity
{
    public SecurityKeyEntity(Guid id, DateTime createdUtc, DateTime modifiedUtc) : base(id, createdUtc, modifiedUtc)
    {
    }
}