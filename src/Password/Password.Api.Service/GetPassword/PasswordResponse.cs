namespace PasswordManager.Password.Api.Service.GetPassword;

[SwaggerSchema(Nullable = false, Required = new[] { "id", "url", "friendlyName", "username", "password" })]
public class PasswordResponse
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("url")]
    public string Url { get; set; }

    [JsonPropertyName("friendlyName")]
    public string FriendlyName { get; set; }

    [JsonPropertyName("username")]
    public string Username { get; set; }

    [JsonPropertyName("password")]
    public string Password { get; set; }

    public PasswordResponse(Guid id, string url, string friendlyName, string username, string password)
    {
        Id = id;
        Url = url;
        FriendlyName = friendlyName;
        Username = username;
        Password = password;
    }
}


