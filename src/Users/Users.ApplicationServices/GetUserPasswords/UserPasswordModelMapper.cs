using PasswordManager.Password.Domain.Password;
using PasswordManager.Users.Domain.User;

namespace PasswordManager.Users.ApplicationServices.GetUserPasswords;
internal static class UserPasswordModelMapper
{
    internal static UserPasswordModel Map(PasswordModel model)
    {
        var userPasswordModel = new UserPasswordModel(model.UserId, model.Id, model.Url, model.FriendlyName, model.Username, model.Password);

        return userPasswordModel;
    }
}
