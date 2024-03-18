using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using PasswordManager.KeyVaults.Api.Service.Models;
using PasswordManager.KeyVaults.ApplicationServices.KeyVaultManager;
using PasswordManager.KeyVaults.Domain.Operations;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PasswordManager.KeyVaults.Api.Service.Endpoints.UnprotectText
{
    public class UnprotectTextEndpoint : EndpointBaseAsync.WithRequest<UnprotectTextRequestWithBody>.WithActionResult<ProtectedTextResponse>
    {
        private readonly IKeyVaultManagerService _keyVaultManagerService;

        public UnprotectTextEndpoint(IKeyVaultManagerService keyVaultManagerService)
        {
            _keyVaultManagerService = keyVaultManagerService;
        }

        [HttpPost("api/keyvaults/unprotect")]
        [ProducesResponseType(typeof(OperationAcceptedResponse), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
        Summary = "Unprotects text",
        Description = "Unprotects text from received objectId and protectedText",
        OperationId = "UnprotectText",
        Tags = new[] { "KeyVault" })
        ]
        public override async Task<ActionResult<ProtectedTextResponse>> HandleAsync([FromQuery] UnprotectTextRequestWithBody request, CancellationToken cancellationToken = default)
        {
            try
            {
                OperationDetails operationDetails = new OperationDetails(request.CreatedByUserId);

                var unprotectedText = await _keyVaultManagerService.RequestUnprotect(request.Details.SecurityKeyId, request.Details.ProtectedText, operationDetails);

                var response = new UnprotectedTextResponse(unprotectedText);

                return Ok(response);
            }
            catch (KeyVaultManagerServiceException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Unexpected error occurred: Could not unprotect requested item.");
            }
        }
    }

    public sealed class UnprotectTextRequestWithBody : SecurityKeyOperationRequest<UnprotectTextRequestDetails>
    {
    }

    [SwaggerSchema(Nullable = false, Required = new[] { "securityKeyId", "protectedText" })]
    public sealed class UnprotectTextRequestDetails
    {
        [JsonPropertyName("securityKeyId")]
        public Guid SecurityKeyId { get; set; }

        [JsonPropertyName("protectedText")]
        public string ProtectedText { get; set; }
    }
}
