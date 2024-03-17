using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PasswordManager.KeyVaults.Api.Service.Models
{
    [SwaggerSchema(Nullable = false, Required = new[] { "unprotectedText" })]
    public class UnprotectedTextResponse
    {
        [JsonPropertyName("unprotectedText")]
        public string UnprotectedText { get; set; }

        public UnprotectedTextResponse(string unprotectedText)
        {
            UnprotectedText = unprotectedText;
        }
    }
}
