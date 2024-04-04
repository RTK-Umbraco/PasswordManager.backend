using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PasswordManager.Users.Api.Service.Models;

[SwaggerSchema(Nullable = false, Required = new[] { "id", "passwordId", "url", "friendlyName", "username", "password" })]
public class UserPasswordResponse
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("passwordId")]
    public Guid PasswordId { get; set; }

    [JsonPropertyName("url")]
    public string Url { get; set; }

    [JsonPropertyName("friendlyName")]
    public string FriendlyName { get; set; }

    [JsonPropertyName("username")]
    public string Username { get; set; }

    [JsonPropertyName("password")]
    public string Password { get; set; }

    public UserPasswordResponse(Guid id, Guid passwordId, string url, string friendlyName, string username, string password)
    {
        Id = id;
        PasswordId = passwordId;
        Url = url;
        FriendlyName = friendlyName;
        Username = username;
        Password = password;
    }
}
