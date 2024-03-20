using PasswordManager.Users.Domain.Users;

namespace PasswordManager.Users.ApplicationServices.GetUser;

public interface IGetUserService
{
    Task<UserModel> GetUser(Guid userId);
}