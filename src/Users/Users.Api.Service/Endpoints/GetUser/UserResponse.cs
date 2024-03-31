using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PasswordManager.Users.Api.Service.Endpoints.GetUser;

[SwaggerSchema(Nullable = false, Required = new[] { "id" })]
public class UserResponse
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    [JsonPropertyName("firebaseId")]
    public Guid FirebaseId { get; set; }

    public UserResponse(Guid id, Guid firebaseId)
    {
        Id = id;
        FirebaseId = firebaseId;
    }
}


