using PasswordManager.Users.Domain.Users;

namespace PasswordManager.Users.Infrastructure.UserRepository;
internal static class UserEntityMapper
{
    internal static UserEntity Map(UserModel model)
    {
        return new UserEntity(
            model.Id,
            model.CreatedUtc,
            model.ModifiedUtc,
            model.FirebaseId
            );
    }

    internal static UserModel Map(UserEntity entity)
    {
        return new UserModel(
            entity.Id,
            entity.CreatedUtc,
            entity.ModifiedUtc,
            entity.Deleted,
            entity.FirebaseId
            );
    }
}
