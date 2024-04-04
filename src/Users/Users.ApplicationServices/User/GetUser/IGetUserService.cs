using PasswordManager.Users.Domain.User;

namespace PasswordManager.Users.ApplicationServices.User.GetUser;

public interface IGetUserService
{
    Task<UserModel> GetUser(Guid userId);
}