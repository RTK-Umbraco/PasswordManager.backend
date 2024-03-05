namespace PasswordManager.Password.Domain.Password;
public class PasswordModel : BaseModel
{
    public PasswordModel(Guid id, DateTime createdUtc, DateTime modifiedUtc) : base(id, createdUtc, modifiedUtc)
    {
    }
}
