using PasswordManager.Users.Infrastructure.BaseRepository;

namespace PasswordManager.Users.Infrastructure.UserRepository;

public class UserEntity : BaseEntity
{
    public Guid FirebaseId { get; }
    public string SecretKey { get; } 
    public UserEntity(Guid id, DateTime createdUtc, DateTime modifiedUtc, Guid firebaseId, string secretKey) : base(id, createdUtc, modifiedUtc)
    {
        FirebaseId = firebaseId;
        SecretKey = secretKey;
    }
}