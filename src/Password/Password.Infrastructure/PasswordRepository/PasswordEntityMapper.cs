using PasswordManager.Password.Domain.Password;

namespace PasswordManager.Password.Infrastructure.PasswordRepository;
internal static class PasswordEntityMapper
{
    internal static PasswordEntity Map(PasswordModel model)
    {
        return new PasswordEntity(
            model.Id,
            model.CreatedUtc,
            model.ModifiedUtc
            );
    }

    internal static PasswordModel Map(PasswordEntity entity)
    {
        return new PasswordModel(
            entity.Id,
            entity.CreatedUtc,
            entity.ModifiedUtc
            );
    }
}
