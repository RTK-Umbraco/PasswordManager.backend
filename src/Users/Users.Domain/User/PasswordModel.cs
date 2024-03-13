namespace PasswordManager.Users.Domain.User;
public sealed class PasswordModel
{
    public Guid UserId { get; }
    public Guid PasswordId { get; }
    public string Url { get; }
    public string FriendlyName { get; }
    public string Username { get; }
    public string Password { get; }

    public PasswordModel(Guid userId, Guid passwordId, string url, string friendlyName, string username, string password)
    {
        UserId = userId;
        PasswordId = passwordId;
        Url = url;
        FriendlyName = friendlyName;
        Username = username;
        Password = password;
    }
}
