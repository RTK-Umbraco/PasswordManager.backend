using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using PasswordManager.KeyVaults.Api.Service.Models;
using PasswordManager.KeyVaults.ApplicationServices.Protection;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PasswordManager.KeyVaults.Api.Service.Endpoints.Unprotect
{
    public class UnprotectEndpoint : EndpointBaseAsync.WithRequest<UnprotectItemRequestDetails>.WithActionResult<ProtectedItemResponse>
    {
        private readonly IProtectionService _protectionService;

        public UnprotectEndpoint(IProtectionService protectionService)
        {
            _protectionService = protectionService;
        }

        [HttpPost("api/keyvaults/unprotect")]
        [ProducesResponseType(typeof(ItemResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
        Summary = "Unprotects item",
        Description = "Unprotects item from received objectId and protectedItem",
        OperationId = "UnprotectItem",
        Tags = new[] { "KeyVault" })
        ]
        public override async Task<ActionResult<ProtectedItemResponse>> HandleAsync([FromBody] UnprotectItemRequestDetails request, CancellationToken cancellationToken = default)
        {
            try
            {

                var protectedItem = new List<string>();

                foreach (var item in request.ProtectedItems)
                {
                    protectedItem.Add(_protectionService.Unprotect(item, request.SecretKey));
                }

                var response = new ItemResponse(protectedItem);

                return Ok(response);
            }
            catch (ProtectionServiceException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Unexpected error occurred: Could not unprotect requested item.");
            }
        }
    }

    [SwaggerSchema(Nullable = false, Required = new[] { "secretKey", "protectedItems" })]
    public sealed class UnprotectItemRequestDetails
    {
        [JsonPropertyName("secretKey")]
        public string SecretKey { get; set; }

        [JsonPropertyName("protectedItems")]
        public IEnumerable<string> ProtectedItems { get; set; }
    }
}
