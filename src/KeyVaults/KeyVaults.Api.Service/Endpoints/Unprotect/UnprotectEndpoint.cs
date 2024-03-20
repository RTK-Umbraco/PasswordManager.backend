using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using PasswordManager.KeyVaults.Api.Service.Models;
using PasswordManager.KeyVaults.ApplicationServices.KeyVaultManager;
using PasswordManager.KeyVaults.Domain.Operations;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PasswordManager.KeyVaults.Api.Service.Endpoints.Unprotect
{
    public class UnprotectEndpoint : EndpointBaseAsync.WithRequest<UnprotectItemRequestWithBody>.WithActionResult<ProtectedItemResponse>
    {
        private readonly IKeyVaultManagerService _keyVaultManagerService;

        public UnprotectEndpoint(IKeyVaultManagerService keyVaultManagerService)
        {
            _keyVaultManagerService = keyVaultManagerService;
        }

        [HttpPost("api/keyvaults/unprotect")]
        [ProducesResponseType(typeof(OperationAcceptedResponse), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
        Summary = "Unprotects item",
        Description = "Unprotects item from received objectId and protectedItem",
        OperationId = "UnprotectItem",
        Tags = new[] { "KeyVault" })
        ]
        public override async Task<ActionResult<ProtectedItemResponse>> HandleAsync([FromQuery] UnprotectItemRequestWithBody request, CancellationToken cancellationToken = default)
        {
            try
            {
                OperationDetails operationDetails = new OperationDetails(request.CreatedByUserId);

                var unprotectedItem = await _keyVaultManagerService.RequestUnprotect(request.Details.SecurityKeyId, request.Details.ProtectedItem, operationDetails);

                var response = new ItemResponse(unprotectedItem);

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

    public sealed class UnprotectItemRequestWithBody : SecurityKeyOperationRequest<UnprotectItemRequestDetails>
    {
    }

    [SwaggerSchema(Nullable = false, Required = new[] { "securityKeyId", "protectedItem" })]
    public sealed class UnprotectItemRequestDetails
    {
        [JsonPropertyName("securityKeyId")]
        public Guid SecurityKeyId { get; set; }

        [JsonPropertyName("protectedItem")]
        public string ProtectedItem { get; set; }
    }
}
