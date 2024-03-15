namespace PasswordManager.Users.Domain.User;
public sealed class UserPasswordModel
{
    public Guid UserId { get; }
    public Guid PasswordId { get; }
    public string Url { get; }
    public string FriendlyName { get; }
    public string Username { get; }
    public string Password { get; }

    public UserPasswordModel(Guid userId, Guid passwordId, string url, string friendlyName, string username, string password)
    {
        UserId = userId;
        PasswordId = passwordId;
        Url = url;
        FriendlyName = friendlyName;
        Username = username;
        Password = password;
    }

    public UserPasswordModel(Guid userId, string url, string friendlyName, string username, string password)
    {
        UserId = userId;
        Url = url;
        FriendlyName = friendlyName;
        Username = username;
        Password = password;
    }

    public static UserPasswordModel CreateUserPassword(Guid userId, string url, string friendlyName, string username, string password)
    {
        var userPasswordModel = new UserPasswordModel(userId, url, friendlyName, username, password);

        return userPasswordModel;
    }
}
