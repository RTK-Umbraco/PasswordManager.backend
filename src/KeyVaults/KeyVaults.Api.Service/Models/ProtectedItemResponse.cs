using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PasswordManager.KeyVaults.Api.Service.Models
{
    [SwaggerSchema(Nullable = false, Required = new[] { "protectedItems" })]
    public class ProtectedItemResponse
    {
        [JsonPropertyName("protectedItems")]
        public IEnumerable<string> ProtectedItems { get; set; }

        public ProtectedItemResponse(IEnumerable<string> protectedItems)
        {
            ProtectedItems = protectedItems;
        }
    }
}
