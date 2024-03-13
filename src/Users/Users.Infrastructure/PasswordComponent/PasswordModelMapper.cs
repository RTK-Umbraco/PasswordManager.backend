namespace PasswordManager.Users.Infrastructure.PasswordComponent;
internal static class PasswordModelMapper
{
    internal static Domain.User.PasswordModel Map(Password.Domain.Password.PasswordModel model)
    {
        var userPasswordModel = new Domain.User.PasswordModel(model.UserId, model.Id, model.Url, model.FriendlyName, model.Username, model.Password);

        return userPasswordModel;
    }
}
