using PasswordManager.Users.Domain.User;

namespace PasswordManager.Users.ApplicationServices.Components;
public interface IPasswordComponent
{
    Task<IEnumerable<PasswordModel>> GetUserPasswords(Guid userId);
}
