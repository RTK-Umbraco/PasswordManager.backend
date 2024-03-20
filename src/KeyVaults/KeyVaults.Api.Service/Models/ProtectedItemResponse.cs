using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PasswordManager.KeyVaults.Api.Service.Models
{
    [SwaggerSchema(Nullable = false, Required = new[] { "securityKeyId", "protectedItem" })]
    public class ProtectedItemResponse
    {
        [JsonPropertyName("securityKeyId")]
        public Guid SecurityKeyId { get; set; }

        [JsonPropertyName("protectedItem")]
        public string ProtectedItem { get; set; }

        public ProtectedItemResponse(Guid securityKeyId, string protectedItem)
        {
            SecurityKeyId = securityKeyId;
            ProtectedItem = protectedItem;
        }
    }
}
