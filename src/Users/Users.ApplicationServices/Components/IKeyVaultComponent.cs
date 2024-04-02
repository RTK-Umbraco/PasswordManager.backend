using PasswordManager.Users.Domain.User;

namespace PasswordManager.Users.ApplicationServices.Components;
public interface IKeyVaultComponent
{
    Task<string> CreateEncryptedPassword(UserPasswordModel userPasswordModel, string secretKey);
    Task<IEnumerable<UserPasswordModel>> DecryptPasswords(IEnumerable<UserPasswordModel> userPasswordModels, string secretKey);
}
