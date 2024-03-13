using PasswordManager.Users.Domain.User;

namespace PasswordManager.Users.ApplicationServices.Components;
public interface IPasswordComponent
{
    Task<IEnumerable<UserPasswordModel>> GetUserPasswords(Guid userId);
    Task<UserPasswordModel> CreateUserPassword(UserPasswordModel userPasswordModel);
}
