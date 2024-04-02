using PasswordManager.Users.Domain.User;

namespace PasswordManager.Users.ApplicationServices.Components;
public interface IPasswordComponent
{
    Task CreateUserPassword(UserPasswordModel userPasswordModel);
    Task<IEnumerable<UserPasswordModel>> GetUserPasswordsFromUrl(Guid userId, string url);
    Task<IEnumerable<UserPasswordModel>> GetUserPasswords(Guid userId);
}
