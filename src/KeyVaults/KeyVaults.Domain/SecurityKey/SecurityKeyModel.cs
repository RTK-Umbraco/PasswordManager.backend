namespace PasswordManager.KeyVaults.Domain.KeyVaults;
public class SecurityKeyModel : BaseModel
{
    public SecurityKeyModel(Guid id, DateTime createdUtc, DateTime modifiedUtc) : base(id, createdUtc, modifiedUtc)
    {
    }
}
