namespace PasswordManager.Password.Domain.Password;
public class PasswordModel : BaseModel
{
    public string Url { get; }
    public string FriendlyName { get; }
    public string Username { get; }
    public string Password { get; }

    public PasswordModel(Guid id, DateTime createdUtc, DateTime modifiedUtc, bool deleted, string url, string friendlyName, string username, string password) 
        : base(id, createdUtc, modifiedUtc, deleted)
    {
        Url = url;
        FriendlyName = friendlyName;
        Username = username;
        Password = password;
    }
}
