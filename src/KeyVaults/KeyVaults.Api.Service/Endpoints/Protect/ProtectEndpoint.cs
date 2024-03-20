using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using PasswordManager.KeyVaults.Api.Service.Models;
using PasswordManager.KeyVaults.ApplicationServices.KeyVaultManager;
using PasswordManager.KeyVaults.Domain.Operations;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PasswordManager.KeyVaults.Api.Service.Endpoints.Protect
{
    public class ProtectEndpoint : EndpointBaseAsync.WithRequest<ProtectItemRequestWithBody>.WithActionResult<ProtectedItemResponse>
    {
        private readonly IKeyVaultManagerService _keyVaultManagerService;

        public ProtectEndpoint(IKeyVaultManagerService keyVaultManagerService)
        {
            _keyVaultManagerService = keyVaultManagerService;
        }

        [HttpPost("api/keyvaults/protect")]
        [ProducesResponseType(typeof(OperationAcceptedResponse), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
        Summary = "Protects an item",
        Description = "Creates a SecurityKey object and returns received item in protected form along with a SecurityKeyId",
        OperationId = "ProtectItem",
        Tags = new[] { "KeyVault" })
        ]
        public override async Task<ActionResult<ProtectedItemResponse>> HandleAsync([FromQuery] ProtectItemRequestWithBody request, CancellationToken cancellationToken = default)
        {
            try
            {
                OperationDetails operationDetails = new OperationDetails(request.CreatedByUserId);

                var protectedItem = await _keyVaultManagerService.RequestProtect(request.Details.Item, operationDetails);

                var response = new ProtectedItemResponse(protectedItem.SecurityKeyId, protectedItem.ProtectedItem);

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

    public sealed class ProtectItemRequestWithBody : SecurityKeyOperationRequest<ProtectItemRequestDetails>
    {
    }

    [SwaggerSchema(Nullable = false, Required = new[] { "item" })]
    public sealed class ProtectItemRequestDetails
    {
        [JsonPropertyName("item")]
        public string Item { get; set; }
    }
}
