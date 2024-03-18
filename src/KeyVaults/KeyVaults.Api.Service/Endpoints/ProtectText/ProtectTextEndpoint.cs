using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using PasswordManager.KeyVaults.Api.Service.Models;
using PasswordManager.KeyVaults.ApplicationServices.KeyVaultManager;
using PasswordManager.KeyVaults.Domain.Operations;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PasswordManager.KeyVaults.Api.Service.Endpoints.ProtectText
{
    public class ProtectTextEndpoint : EndpointBaseAsync.WithRequest<ProtectTextRequestWithBody>.WithActionResult<ProtectedTextResponse>
    {
        private readonly IKeyVaultManagerService _keyVaultManagerService;

        public ProtectTextEndpoint(IKeyVaultManagerService keyVaultManagerService)
        {
            _keyVaultManagerService = keyVaultManagerService;
        }

        [HttpPost("api/keyvaults/protect")]
        [ProducesResponseType(typeof(OperationAcceptedResponse), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
        Summary = "Protects text",
        Description = "Creates a SecurityKey object and returns received text in protected form",
        OperationId = "ProtectText",
        Tags = new[] { "KeyVault" })
        ]
        public override async Task<ActionResult<ProtectedTextResponse>> HandleAsync([FromQuery] ProtectTextRequestWithBody request, CancellationToken cancellationToken = default)
        {
            try
            {
                OperationDetails operationDetails = new OperationDetails(request.CreatedByUserId);

                var protectedText = await _keyVaultManagerService.RequestProtect(request.Details.ObjectId, request.Details.PlainText, operationDetails);

                var response = new ProtectedTextResponse(protectedText);

                return Ok(response);
            }
            catch (KeyVaultManagerServiceException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Unexpected error occurred: Could not protect requested item.");
            }
        }
    }

    public sealed class ProtectTextRequestWithBody : SecurityKeyOperationRequest<ProtectTextRequestDetails>
    {
    }

    [SwaggerSchema(Nullable = false, Required = new[] { "objectId", "plainText" })]
    public sealed class ProtectTextRequestDetails
    {
        [JsonPropertyName("objectId")]
        public Guid ObjectId { get; set; }

        [JsonPropertyName("plainText")]
        public string PlainText { get; set; }
    }
}
