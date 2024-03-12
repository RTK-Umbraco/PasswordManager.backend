using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PasswordManager.Users.Api.Service.GetUser;

[SwaggerSchema(Nullable = false, Required = new[] { "id" })]
public class UserResponse
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    public UserResponse(Guid id)
    {
        Id = id;
    }
}


