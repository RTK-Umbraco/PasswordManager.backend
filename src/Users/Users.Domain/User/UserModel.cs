namespace PasswordManager.Users.Domain.Users;
public class UserModel : BaseModel
{
    public string FirebaseId { get; }
    public string SecretKey { get; }
    public UserModel(Guid id, DateTime createdUtc, DateTime modifiedUtc, bool deleted, string firebaseId, string secretKey) : base(id, createdUtc, modifiedUtc, deleted)
    {
        FirebaseId = firebaseId;
        SecretKey = secretKey;
    }
}
