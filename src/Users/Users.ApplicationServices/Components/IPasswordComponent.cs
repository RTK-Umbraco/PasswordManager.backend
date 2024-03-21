using PasswordManager.Users.Domain.User;

namespace PasswordManager.Users.ApplicationServices.Components;
public interface IPasswordComponent
{
    Task CreateUserPassword(UserPasswordModel userPasswordModel);
    Task<IEnumerable<UserPasswordModel>> GetUserPassword(Guid userId, string url);
}
