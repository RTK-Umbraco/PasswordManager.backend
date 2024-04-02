using PasswordManager.Users.Domain.User;

namespace PasswordManager.Users.Infrastructure.UserRepository;
internal static class UserEntityMapper
{
    internal static UserEntity Map(UserModel model)
    {
        return new UserEntity(
            model.Id,
            model.CreatedUtc,
            model.ModifiedUtc,
            model.FirebaseId,
            model.SecretKey
            );
    }

    internal static UserModel Map(UserEntity entity)
    {
        return new UserModel(
            entity.Id,
            entity.CreatedUtc,
            entity.ModifiedUtc,
            entity.Deleted,
            entity.FirebaseId,
            entity.SecretKey
            );
    }
}
