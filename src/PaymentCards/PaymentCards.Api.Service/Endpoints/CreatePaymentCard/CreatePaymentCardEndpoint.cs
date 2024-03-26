using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PasswordManager.PaymentCards.Api.Service.Endpoints.GetOperation;
using PasswordManager.PaymentCards.Api.Service.Models;
using PasswordManager.PaymentCards.ApplicationServices.PaymentCard.CreatePaymentCard;
using PasswordManager.PaymentCards.Domain.Operations;
using PasswordManager.PaymentCards.Domain.PaymentCards;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PasswordManager.PaymentCards.Api.Service.Endpoints.CreatePaymentCard
{
    public class CreatePaymentCardEndpoint : EndpointBaseAsync.WithRequest<CreatePaymentCardRequestWithBody>.WithoutResult
    {
        private readonly ICreatePaymentCardService _createPaymentCardService;

        public CreatePaymentCardEndpoint(ICreatePaymentCardService createPaymentCardService)
        {
            _createPaymentCardService = createPaymentCardService;
        }

        [HttpPost("api/paymentcard")]
        [ProducesResponseType(typeof(OperationAcceptedResponse), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
        Summary = "Creates paymentcard",
        Description = "Creates a paymentcard",
        OperationId = "CreatePaymentCard",
        Tags = new[] { "PaymentCard" })
        ]
        public override async Task<ActionResult> HandleAsync([FromBody] CreatePaymentCardRequestWithBody request, CancellationToken cancellationToken = default)
        {
            var paymentCardModel = PaymentCardModel.CreatePaymentCard(
                request.Details.UserId,
                request.Details.CardNumber,
                request.Details.CardHolderName,
                request.Details.ExpirationDate
                );

            var operationResult = await _createPaymentCardService.RequestCreatePaymentCard(paymentCardModel, new OperationDetails(request.CreatedByUserId));

            return operationResult.Status switch
            {
                OperationResultStatus.Accepted => new AcceptedResult(
                    new Uri(GetOperationByRequestIdEndpoint.GetRelativePath(operationResult.GetOperation()), UriKind.Relative),
                    new OperationAcceptedResponse(operationResult.GetOperation().RequestId)),

                OperationResultStatus.InvalidOperationRequest => Problem(title: "Can't create paymentcard for user", detail: operationResult.GetMessage(),
                    statusCode: StatusCodes.Status400BadRequest),
                _ => Problem(title: "Unknown error requesting to create payment card", detail: "Unknown error - check logs",
                    statusCode: StatusCodes.Status500InternalServerError),
            };
        }
    }

    public sealed class  CreatePaymentCardRequestWithBody : UserOperationRequest<CreatePaymentCardRequestDetails>
    {
    }

    public sealed class CreatePaymentCardRequestDetails
    {
        [JsonPropertyName("userId")]
        public Guid UserId { get; set; }

        [JsonPropertyName("cardNumber")]
        public string CardNumber { get; set; }

        [JsonPropertyName("cardHolderName")]
        public string CardHolderName { get; set; }

        [JsonPropertyName("expirationDate")]
        public string ExpirationDate { get; set; }
    }
}
