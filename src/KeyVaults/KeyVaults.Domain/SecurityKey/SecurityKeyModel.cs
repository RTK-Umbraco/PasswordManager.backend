namespace PasswordManager.KeyVaults.Domain.KeyVaults;
public class SecurityKeyModel : BaseModel
{
    public string SecretKey { get; private set; }
    public Guid ObjectId { get; private set; }

    public SecurityKeyModel(Guid id, string secretKey, Guid objectId) : base(id)
    {
        SecretKey = secretKey;
        ObjectId = objectId;
    }

    public SecurityKeyModel(Guid id, DateTime createdUtc, DateTime modifiedUtc, bool deleted,
        string secretKey, Guid objectId) : base(id, createdUtc, modifiedUtc, deleted)
    {
        SecretKey = secretKey;
        ObjectId = objectId;
    }

    public static SecurityKeyModel Create(string secretKey, Guid objectId)
    {
        return new SecurityKeyModel(Guid.NewGuid(), secretKey, objectId);
    }

    public static SecurityKeyModel UpdateSecurityKey(Guid id, string secretKey, Guid objectId)
    {
        return new SecurityKeyModel(id, secretKey, objectId);
    }

    public void UpdateSecretKey(string secretKey)
    {
        SecretKey = secretKey;
    }
}
