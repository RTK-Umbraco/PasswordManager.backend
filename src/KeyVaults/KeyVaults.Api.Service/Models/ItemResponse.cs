using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PasswordManager.KeyVaults.Api.Service.Models
{
    [SwaggerSchema(Nullable = false, Required = new[] { "item" })]
    public class ItemResponse
    {
        [JsonPropertyName("item")]
        public string Item { get; set; }

        public ItemResponse(string item)
        {
            Item = item;
        }
    }
}
