using PasswordManager.KeyVaults.Infrastructure.BaseRepository;

namespace PasswordManager.KeyVaults.Infrastructure.SecurityKeyRepository;

public class SecurityKeyEntity : BaseEntity
{
    public string SecretKey { get; private set; }
    public Guid ObjectId { get; private set; }

    public SecurityKeyEntity(
        Guid id, 
        DateTime createdUtc, 
        DateTime modifiedUtc,
        bool deleted,
        string secretKey,
        Guid objectId) : base(id, createdUtc, modifiedUtc, deleted)
    {
        SecretKey = secretKey;
        ObjectId = objectId;
    }
}