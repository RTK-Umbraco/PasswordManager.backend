using PasswordManager.Users.Infrastructure.BaseRepository;

namespace PasswordManager.Users.Infrastructure.UserRepository;

public class UserEntity : BaseEntity
{
    public Guid FirebaseId { get; }
    public UserEntity(Guid id, DateTime createdUtc, DateTime modifiedUtc, Guid firebaseId) : base(id, createdUtc, modifiedUtc)
    {
        FirebaseId = firebaseId;
    }
}