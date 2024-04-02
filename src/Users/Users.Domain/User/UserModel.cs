namespace PasswordManager.Users.Domain.User;
public class UserModel : BaseModel
{
    public Guid FirebaseId { get; }
    public string SecretKey { get; }
    public UserModel(Guid id, DateTime createdUtc, DateTime modifiedUtc, bool deleted, Guid firebaseId, string secretKey) : base(id, createdUtc, modifiedUtc, deleted)
    {
        FirebaseId = firebaseId;
        SecretKey = secretKey;
    }
}
