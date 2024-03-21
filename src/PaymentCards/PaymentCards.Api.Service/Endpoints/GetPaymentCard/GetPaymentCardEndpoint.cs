using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace PasswordManager.PaymentCards.Api.Service.Endpoints.GetPaymentCard;

public class GetPaymentCardEndpoint : EndpointBaseAsync.WithRequest<Guid>.WithActionResult<PaymentCardResponse>
{
    public GetPaymentCardEndpoint()
    {
    }

    [HttpGet("api/paymentcards/{paymentcardId:guid}")]
    [ProducesResponseType(typeof(PaymentCardResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [SwaggerOperation(
        Summary = "Get PaymentCard by PaymentCard id",
        Description = "Get PaymentCard by PaymentCard id",
        OperationId = "GetPaymentCard",
        Tags = new[] { "PaymentCard" })
    ]
    public override async Task<ActionResult<PaymentCardResponse>> HandleAsync([FromRoute] Guid paymentcardId, CancellationToken cancellationToken = default)
    {
        return new ActionResult<PaymentCardResponse>(new PaymentCardResponse(paymentcardId));
    }
}
