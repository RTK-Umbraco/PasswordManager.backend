using PasswordManager.Users.Domain.User;

namespace PasswordManager.Users.ApplicationServices.Components;
public interface IPasswordComponent
{
    Task CreateUserPassword(UserPasswordModel userPasswordModel);
    Task<IEnumerable<UserPasswordModel>> GetUserPasswordsFromUrl(Guid userId, string url);
    Task<IEnumerable<UserPasswordModel>> GetUserPasswords(Guid userId);
    Task DeleteUserPassword(Guid passwordId, string createdByUserId);
    Task UpdateUserPassword(UserPasswordModel userPasswordModel);
    Task<string> GenerateUserPassword(int length);
}
