using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using PasswordManager.KeyVaults.Api.Service.Models;
using PasswordManager.KeyVaults.ApplicationServices.Protection;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PasswordManager.KeyVaults.Api.Service.Endpoints.Protect
{
    public class ProtectEndpoint : EndpointBaseAsync.WithRequest<ProtectItemRequestDetails>.WithActionResult<ProtectedItemResponse>
    {
        private readonly IProtectionService _protectionService;

        public ProtectEndpoint(IProtectionService protectionService)
        {
            _protectionService = protectionService;
        }

        [HttpPost("api/keyvaults/protect")]
        [ProducesResponseType(typeof(ProtectedItemResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
        Summary = "Protects an item",
        Description = "Returns received item in protected form",
        OperationId = "ProtectItem",
        Tags = new[] { "KeyVault" })
        ]
        public override async Task<ActionResult<ProtectedItemResponse>> HandleAsync([FromBody] ProtectItemRequestDetails request, CancellationToken cancellationToken = default)
        {
            try
            {
                var protectedItem = new List<string>();

                foreach (var item in request.Items)
                {
                    protectedItem.Add(_protectionService.Protect(item, request.SecretKey));
                }

                var response = new ProtectedItemResponse(protectedItem);

                return Ok(response);
            }
            catch (ProtectionServiceException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Unexpected error occurred: Could not protect requested item.");
            }
        }
    }

    [SwaggerSchema(Nullable = false, Required = new[] { "secretKey", "items" })]
    public sealed class ProtectItemRequestDetails
    {
        [JsonPropertyName("secretKey")]
        public string SecretKey { get; set; }

        [JsonPropertyName("items")]
        public IEnumerable<string> Items { get; set; }
    }
}
