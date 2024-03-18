using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using PasswordManager.KeyVaults.Api.Service.Models;
using PasswordManager.KeyVaults.ApplicationServices.KeyVaultManager;
using PasswordManager.KeyVaults.Domain.Operations;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PasswordManager.KeyVaults.Api.Service.Endpoints.ReprotectText
{
    public class ReprotectTextEndpoint : EndpointBaseAsync.WithRequest<ReprotectTextRequestWithBody>.WithActionResult<ProtectedTextResponse>
    {
        private readonly IKeyVaultManagerService _keyVaultManagerService;

        public ReprotectTextEndpoint(IKeyVaultManagerService keyVaultManagerService)
        {
            _keyVaultManagerService = keyVaultManagerService;
        }

        [HttpPost("api/keyvaults/reprotect")]
        [ProducesResponseType(typeof(OperationAcceptedResponse), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
        Summary = "Reprotects text",
        Description = "Generates a new secretkey and returns the protected object in a new form",
        OperationId = "ReprotectText",
        Tags = new[] { "KeyVault" })
        ]
        public override async Task<ActionResult<ProtectedTextResponse>> HandleAsync([FromQuery] ReprotectTextRequestWithBody request, CancellationToken cancellationToken = default)
        {
            try
            {
                OperationDetails operationDetails = new OperationDetails(request.CreatedByUserId);

                var protectedText = await _keyVaultManagerService.RequestReprotect(request.Details.SecurityKeyId, request.Details.ProtectedText, operationDetails);

                var response = new ProtectedTextResponse(request.Details.SecurityKeyId, protectedText);

                return Ok(response);
            }
            catch (KeyVaultManagerServiceException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Unexpected error occurred: Could not reprotect requested item.");
            }
        }
    }

    public sealed class ReprotectTextRequestWithBody : SecurityKeyOperationRequest<ReprotectTextRequestDetails>
    { 
    }

    [SwaggerSchema(Nullable = false, Required = new[] { "securityKeyId", "protectedText" })]
    public sealed class ReprotectTextRequestDetails
    {
        [JsonPropertyName("securityKeyId")]
        public Guid SecurityKeyId { get; set; }

        [JsonPropertyName("protectedText")]
        public string ProtectedText { get; set; }
    }
}
