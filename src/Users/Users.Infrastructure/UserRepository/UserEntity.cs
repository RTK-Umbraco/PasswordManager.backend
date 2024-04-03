using PasswordManager.Users.Infrastructure.BaseRepository;
using System;

namespace PasswordManager.Users.Infrastructure.UserRepository;

public class UserEntity : BaseEntity
{
    public string FirebaseId { get; }
    public string SecretKey { get; } 
    public UserEntity(Guid id, DateTime createdUtc, DateTime modifiedUtc, string firebaseId, string secretKey) : base(id, createdUtc, modifiedUtc)
    {
        FirebaseId = firebaseId;
        SecretKey = secretKey;
    }
}