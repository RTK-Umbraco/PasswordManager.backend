namespace PasswordManager.Password.Domain.Password;
public class PasswordModel : BaseModel
{
    public string Url { get; }
    public string Label { get; }
    public string Username { get; }
    public string Key { get; }

    public PasswordModel(Guid id, string url, string label, string username, string key) 
        : base(id)
    {
        Url = url;
        Label = label;
        Username = username;
        Key = key;
    }

    public static PasswordModel CreatePassword(string url, string label, string username, string key)
    {
        return new PasswordModel(Guid.NewGuid(), url, label, username, key);
    }

    public static PasswordModel UpdatePassword(Guid id, string url, string label, string username, string key)
    {
        return new PasswordModel(id, url, label, username, key);
    }
}
