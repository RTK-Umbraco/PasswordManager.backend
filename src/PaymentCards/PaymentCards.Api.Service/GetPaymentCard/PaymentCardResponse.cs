using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PasswordManager.PaymentCards.Api.Service.GetPaymentCard;

[SwaggerSchema(Nullable = false, Required = new[] { "id" })]
public class PaymentCardResponse
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    public PaymentCardResponse(Guid id)
    {
        Id = id;
    }
}


