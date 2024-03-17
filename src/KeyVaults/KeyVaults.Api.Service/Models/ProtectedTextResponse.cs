using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PasswordManager.KeyVaults.Api.Service.Models
{
    [SwaggerSchema(Nullable = false, Required = new[] { "protectedText" })]
    public class ProtectedTextResponse
    {
        [JsonPropertyName("protectedText")]
        public string ProtectedText { get; set; }

        public ProtectedTextResponse(string protectedText)
        {
            ProtectedText = protectedText;
        }
    }
}
