using PasswordManager.KeyVaults.Domain.KeyVaults;

namespace PasswordManager.KeyVaults.Infrastructure.SecurityKeyRepository;
internal static class SecurityKeyEntityMapper
{
    internal static SecurityKeyEntity Map(SecurityKeyModel model)
    {
        return new SecurityKeyEntity(
            model.Id,
            model.CreatedUtc,
            model.ModifiedUtc,
            model.Deleted,
            model.SecretKey,
            model.ObjectId
            );
    }

    internal static SecurityKeyModel Map(SecurityKeyEntity entity)
    {
        return new SecurityKeyModel(
            entity.Id,
            entity.CreatedUtc,
            entity.ModifiedUtc,
            entity.Deleted,
            entity.SecretKey,
            entity.ObjectId
            );
    }
}
