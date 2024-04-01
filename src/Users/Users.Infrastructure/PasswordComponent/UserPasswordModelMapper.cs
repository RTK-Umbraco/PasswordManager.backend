
using PasswordManager.Users.Domain.User;
using Umbraco.Cloud.Passwordmanager.Password.Api.Client;

namespace PasswordManager.Users.Infrastructure.PasswordComponent;
internal static class UserPasswordModelMapper
{
    internal static UserPasswordModel Map(PasswordResponse model)
    {
        var userPasswordModel = new UserPasswordModel(model.UserId, model.Id, model.Url, model.FriendlyName, model.Username, model.Password);
        return userPasswordModel;
    }

}
