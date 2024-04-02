using PasswordManager.Users.Domain.User;

namespace PasswordManager.Users.ApplicationServices.GetUserPasswords;
public interface IGetUserPasswordsService
{
    Task<IEnumerable<UserPasswordModel>> GetUserPasswordsByUrl(Guid userId, string url);
}
