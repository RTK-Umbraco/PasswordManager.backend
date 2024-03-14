using PasswordManager.KeyVaults.Domain.KeyVaults;

namespace PasswordManager.KeyVaults.Infrastructure.SecurityKeyRepository;
internal static class SecurityKeyEntityMapper
{
    internal static SecurityKeyEntity Map(SecurityKeyModel model)
    {
        return new SecurityKeyEntity(
            model.Id,
            model.CreatedUtc,
            model.ModifiedUtc
            );
    }

    internal static SecurityKeyModel Map(SecurityKeyEntity entity)
    {
        return new SecurityKeyModel(
            entity.Id,
            entity.CreatedUtc,
            entity.ModifiedUtc
            );
    }
}
