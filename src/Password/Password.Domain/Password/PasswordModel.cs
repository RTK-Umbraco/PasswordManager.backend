namespace PasswordManager.Password.Domain.Password;
public class PasswordModel : BaseModel
{
    public string FriendlyName { get; }
    public string Url { get; }
    public string Username { get; }
    public string Password { get; }

    public PasswordModel(Guid id, DateTime createdUtc, DateTime modifiedUtc, bool deleted, string friendlyName, string url, string username, string password) 
        : base(id, createdUtc, modifiedUtc, deleted)
    {
        FriendlyName = friendlyName;
        Url = url;
        Username = username;
        Password = password;
    }
}
