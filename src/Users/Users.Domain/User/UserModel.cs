namespace PasswordManager.Users.Domain.Users;
public class UserModel : BaseModel
{
    public Guid FirebaseId { get; }

    public UserModel(Guid id, DateTime createdUtc, DateTime modifiedUtc, bool deleted, Guid firebaseId) : base(id, createdUtc, modifiedUtc, deleted)
    {
        FirebaseId = firebaseId;
    }
}
