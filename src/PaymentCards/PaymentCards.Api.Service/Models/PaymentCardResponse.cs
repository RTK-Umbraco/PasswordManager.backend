using Swashbuckle.AspNetCore.Annotations;

namespace PasswordManager.PaymentCards.Api.Service.Models
{
    [SwaggerSchema(Nullable = false, Required = new[] { "id", "url", "friendlyName", "username", "password" })]
    public class PaymentCardResponse
    {

    }
}
