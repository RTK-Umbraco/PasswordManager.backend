using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using PasswordManager.KeyVaults.Api.Service.Models;
using PasswordManager.KeyVaults.ApplicationServices.KeyVaultManager;
using PasswordManager.KeyVaults.Domain.Operations;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PasswordManager.KeyVaults.Api.Service.Endpoints.Reprotect
{
    public class ReprotectEndpoint : EndpointBaseAsync.WithRequest<ReprotectItemRequestWithBody>.WithActionResult<ProtectedItemResponse>
    {
        private readonly IKeyVaultManagerService _keyVaultManagerService;

        public ReprotectEndpoint(IKeyVaultManagerService keyVaultManagerService)
        {
            _keyVaultManagerService = keyVaultManagerService;
        }

        [HttpPost("api/keyvaults/reprotect")]
        [ProducesResponseType(typeof(OperationAcceptedResponse), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
        Summary = "Reprotects",
        Description = "Generates a new secretkey and returns the protected item in a new form",
        OperationId = "Reprotect",
        Tags = new[] { "KeyVault" })
        ]
        public override async Task<ActionResult<ProtectedItemResponse>> HandleAsync([FromQuery] ReprotectItemRequestWithBody request, CancellationToken cancellationToken = default)
        {
            try
            {
                OperationDetails operationDetails = new OperationDetails(request.CreatedByUserId);

                var protectedItem = await _keyVaultManagerService.RequestReprotect(request.Details.SecurityKeyId, request.Details.ProtectedItem, operationDetails);

                var response = new ProtectedItemResponse(request.Details.SecurityKeyId, protectedItem);

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

    public sealed class ReprotectItemRequestWithBody : SecurityKeyOperationRequest<ReprotectItemRequestDetails>
    { 
    }

    [SwaggerSchema(Nullable = false, Required = new[] { "securityKeyId", "protectedItem" })]
    public sealed class ReprotectItemRequestDetails
    {
        [JsonPropertyName("securityKeyId")]
        public Guid SecurityKeyId { get; set; }

        [JsonPropertyName("protectedItem")]
        public string ProtectedItem { get; set; }
    }
}
