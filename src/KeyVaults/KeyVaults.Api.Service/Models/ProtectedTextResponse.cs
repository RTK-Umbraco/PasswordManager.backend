using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PasswordManager.KeyVaults.Api.Service.Models
{
    [SwaggerSchema(Nullable = false, Required = new[] { "securityKeyId", "protectedText" })]
    public class ProtectedTextResponse
    {
        [JsonPropertyName("securityKeyId")]
        public Guid SecurityKeyId { get; set; }

        [JsonPropertyName("protectedText")]
        public string ProtectedText { get; set; }

        public ProtectedTextResponse(Guid securityKeyId, string protectedText)
        {
            SecurityKeyId = securityKeyId;
            ProtectedText = protectedText;
        }
    }
}
