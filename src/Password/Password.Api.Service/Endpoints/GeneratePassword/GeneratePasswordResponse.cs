using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PasswordManager.Password.Api.Service.Endpoints.GeneratePassword
{
    [SwaggerSchema(Nullable = false, Required = new[] { "password" })]
    public class GeneratePasswordResponse
    {
        [JsonPropertyName("password")]
        public string Password { get; set; }

        public GeneratePasswordResponse(string password)
        {
            Password = password;
        }
    }
}
