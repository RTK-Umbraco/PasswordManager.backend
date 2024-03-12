namespace PasswordManager.Users.Domain.Users;
public class UserModel : BaseModel
{
    public Guid FirebaseId { get; }

    public UserModel(Guid id, DateTime createdUtc, DateTime modifiedUtc) : base(id, createdUtc, modifiedUtc)
    {
    }
    
    public UserModel(Guid id, DateTime createdUtc, DateTime modifiedUtc, Guid firebaseId) : base(id, createdUtc, modifiedUtc)
    {
        FirebaseId = firebaseId;
    }
}
