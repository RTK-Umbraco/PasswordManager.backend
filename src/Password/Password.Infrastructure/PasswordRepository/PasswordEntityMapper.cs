using PasswordManager.Password.Domain.Password;

namespace PasswordManager.Password.Infrastructure.PasswordRepository;
internal static class PasswordEntityMapper
{
    internal static PasswordEntity Map(PasswordModel model)
    {
        return new PasswordEntity(
            model.Id,
            model.CreatedUtc,
            model.ModifiedUtc,
            model.FriendlyName,
            model.Url,
            model.Username,
            model.Password
            );
    }

    internal static PasswordModel Map(PasswordEntity entity)
    {
        return new PasswordModel(
            entity.Id,
            entity.CreatedUtc,
            entity.ModifiedUtc,
            entity.Deleted,
            entity.FriendlyName,
            entity.Url,
            entity.Username,
            entity.Password
            );
    }
}
