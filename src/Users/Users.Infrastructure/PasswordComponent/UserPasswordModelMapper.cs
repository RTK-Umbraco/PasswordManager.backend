
namespace PasswordManager.Users.Infrastructure.PasswordComponent;
internal static class UserPasswordModelMapper
{
    internal static Domain.User.UserPasswordModel Map(Password.Domain.Password.PasswordModel model)
    {
        var userPasswordModel = new Domain.User.UserPasswordModel(model.UserId, model.Id, model.Url, model.FriendlyName, model.Username, model.Password);
        return userPasswordModel;
    }

    internal static Password.Domain.Password.PasswordModel Map(Domain.User.UserPasswordModel model)
    {
        var userPasswordModel = new Password.Domain.Password.PasswordModel(model.PasswordId, model.UserId, model.Url, model.FriendlyName, model.Username, model.Password);

        return userPasswordModel;
    }
}
