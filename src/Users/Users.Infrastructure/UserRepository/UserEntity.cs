using PasswordManager.Users.Infrastructure.BaseRepository;

namespace PasswordManager.Users.Infrastructure.UserRepository;

public class UserEntity : BaseEntity
{
    public UserEntity(Guid id, DateTime createdUtc, DateTime modifiedUtc) : base(id, createdUtc, modifiedUtc)
    {
    }
}