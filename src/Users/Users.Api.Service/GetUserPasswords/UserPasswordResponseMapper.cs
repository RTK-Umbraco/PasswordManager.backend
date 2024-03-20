using PasswordManager.Users.Domain.User;

namespace PasswordManager.Users.Api.Service.GetUserPasswords;

internal static class UserPasswordResponseMapper
{
    internal static UserPasswordResponse Map(UserPasswordModel model)
    {
        var userPasswordResponse = new UserPasswordResponse(model.UserId, model.PasswordId, model.Url, model.FriendlyName, model.Username, model.Password);

        return userPasswordResponse;
    }
}
