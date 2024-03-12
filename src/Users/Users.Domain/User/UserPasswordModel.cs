namespace PasswordManager.Users.Domain.User;
public sealed class UserPasswordModel
{
    public Guid Id { get; }
    public Guid PasswordId { get; }
    public string Url { get; }
    public string FriendlyName { get; }
    public string Username { get; }
    public string Password { get; }

    public UserPasswordModel(Guid id, Guid passwordId, string url, string friendlyName, string username, string password)
    {
        Id = id;
        PasswordId = passwordId;
        Url = url;
        FriendlyName = friendlyName;
        Username = username;
        Password = password;
    }
}
