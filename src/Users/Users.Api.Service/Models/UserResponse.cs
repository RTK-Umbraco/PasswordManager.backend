using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PasswordManager.Users.Api.Service.Models;

[SwaggerSchema(Nullable = false, Required = new[] { "id" })]
public class UserResponse
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    [JsonPropertyName("firebaseId")]
    public string FirebaseId { get; set; }

    public UserResponse(Guid id, string firebaseId)
    {
        Id = id;
        FirebaseId = firebaseId;
    }
}


