using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PasswordManager.KeyVaults.Api.Service.GetSecurityKey;

[SwaggerSchema(Nullable = false, Required = new[] { "id" })]
public class SecurityKeyResponse
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    public SecurityKeyResponse(Guid id)
    {
        Id = id;
    }
}


