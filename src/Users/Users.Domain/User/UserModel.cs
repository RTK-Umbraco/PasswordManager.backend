namespace PasswordManager.Users.Domain.Users;
public class UserModel : BaseModel
{
    public UserModel(Guid id, DateTime createdUtc, DateTime modifiedUtc) : base(id, createdUtc, modifiedUtc)
    {
    }
}
