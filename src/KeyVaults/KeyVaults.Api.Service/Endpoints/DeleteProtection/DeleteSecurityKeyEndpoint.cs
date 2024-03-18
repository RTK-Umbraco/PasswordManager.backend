using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using PasswordManager.KeyVaults.Api.Service.Models;
using PasswordManager.KeyVaults.ApplicationServices.DeleteSecurityKey;
using PasswordManager.KeyVaults.Domain.Operations;
using Swashbuckle.AspNetCore.Annotations;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json.Serialization;

namespace PasswordManager.KeyVaults.Api.Service.Endpoints.DeleteProtection
{
    public sealed class DeleteSecurityKeyEndpoint : EndpointBaseAsync.WithRequest<DeleteSecurityKeyRequestWithBody>.WithoutResult
    {
        private readonly IDeleteSecurityKeyService _deleteSecurityKeyService;

        public DeleteSecurityKeyEndpoint(IDeleteSecurityKeyService deleteSecurityKeyService)
        {
            _deleteSecurityKeyService = deleteSecurityKeyService;
        }

        [HttpDelete("api/keyvaults/delete")]
        [ProducesResponseType(typeof(OperationAcceptedResponse), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
        Summary = "Deletes SecurityKey",
        Description = "Deletes the SecurityKey tied to the requested objectId",
        OperationId = "DeleteSecurityKey",
        Tags = new[] { "KeyVault" })
        ]
        public override async Task<ActionResult> HandleAsync([FromQuery] DeleteSecurityKeyRequestWithBody request, CancellationToken cancellationToken = default)
        {
            var operationResult = await _deleteSecurityKeyService.RequestDeleteSecurityKey(request.Details.ObjectId, new OperationDetails(request.CreatedByUserId));

            return operationResult.Status switch
            {
                OperationResultStatus.Accepted => Ok(),

                OperationResultStatus.InvalidOperationRequest => Problem(title: "Could not delete protection", detail: operationResult.GetMessage(),
                    statusCode: StatusCodes.Status400BadRequest),
                _ => Problem(title: "Unknown error deleting protection", detail: "Unknown error - check logs",
                    statusCode: StatusCodes.Status500InternalServerError),
            };
        }
    }

    public sealed class DeleteSecurityKeyRequestWithBody : SecurityKeyOperationRequest<DeleteSecurityKeyRequest>
    {
    }

    [SwaggerSchema(Nullable = false, Required = new[] { "objectId" })]
    public sealed class DeleteSecurityKeyRequest
    {
        [JsonPropertyName("objectId")]
        public Guid ObjectId { get; set; }
    }
}
