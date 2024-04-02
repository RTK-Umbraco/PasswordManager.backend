using PasswordManager.Users.Api.Service.Models;
using PasswordManager.Users.Domain.Users;

namespace PasswordManager.Users.Api.Service;

internal static class UserResponseMapper
{
    internal static UserResponse Map(UserModel userModel)
    {
        var passwordResponse = new UserResponse(userModel.Id, userModel.FirebaseId);

        return passwordResponse;
    }

    internal static IEnumerable<UserResponse> Map(IEnumerable<UserModel> userModels)
    {
        return userModels.Select(Map);
    }
}
