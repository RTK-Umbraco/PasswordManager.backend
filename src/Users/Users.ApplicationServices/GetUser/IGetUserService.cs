using PasswordManager.Users.Domain.User;

namespace PasswordManager.Users.ApplicationServices.GetUser;

public interface IGetUserService
{
    Task<UserModel> GetUser(Guid userId);
}