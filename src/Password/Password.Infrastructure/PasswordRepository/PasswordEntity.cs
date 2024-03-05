using PasswordManager.Password.Infrastructure.BaseRepository;

namespace PasswordManager.Password.Infrastructure.PasswordRepository;

public class PasswordEntity : BaseEntity
{
    public string FriendlyName { get; }
    public string Url { get; }
    public string Username { get; }
    public string Password { get; }

    public PasswordEntity(Guid id, DateTime createdUtc, DateTime modifiedUtc, string friendlyName, string url, string username, string password) 
        : base(id, createdUtc, modifiedUtc)
    {
        FriendlyName = friendlyName;
        Url = url;
        Username = username;
        Password = password;
    }
}