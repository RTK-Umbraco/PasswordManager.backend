namespace PasswordManager.KeyVaults.Domain.KeyVaults;
public class SecurityKeyModel : BaseModel
{
    public string SecretKey { get; private set; }

    public SecurityKeyModel(Guid id, string secretKey) : base(id)
    {
        SecretKey = secretKey;
    }

    public SecurityKeyModel(Guid id, DateTime createdUtc, DateTime modifiedUtc, bool deleted,
        string secretKey) : base(id, createdUtc, modifiedUtc, deleted)
    {
        SecretKey = secretKey;
    }

    public static SecurityKeyModel Create(string secretKey)
    {
        return new SecurityKeyModel(Guid.NewGuid(), secretKey);
    }

    public static SecurityKeyModel UpdateSecurityKey(Guid id, string secretKey)
    {
        return new SecurityKeyModel(id, secretKey);
    }

    public void UpdateSecretKey(string secretKey)
    {
        SecretKey = secretKey;
    }
}
