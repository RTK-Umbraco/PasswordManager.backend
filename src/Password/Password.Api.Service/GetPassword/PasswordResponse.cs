using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PasswordManager.Password.Api.Service.GetPassword;

[SwaggerSchema(Nullable = false, Required = new[] { "id" })]
public class PasswordResponse
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    public PasswordResponse(Guid id)
    {
        Id = id;
    }
}


