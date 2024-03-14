using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace PasswordManager.KeyVaults.Api.Service.GetSecurityKey;

public class GetSecurityKeyEndpoint : EndpointBaseAsync.WithRequest<Guid>.WithActionResult<SecurityKeyResponse>
{
    public GetSecurityKeyEndpoint()
    {
    }

    [HttpGet("api/securitykeys/{securitykeyId:guid}")]
    [ProducesResponseType(typeof(SecurityKeyResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [SwaggerOperation(
        Summary = "Get SecurityKey by SecurityKey id",
        Description = "Get SecurityKey by SecurityKey id",
        OperationId = "GetSecurityKey",
        Tags = new[] { "SecurityKey" })
    ]
    public override async Task<ActionResult<SecurityKeyResponse>> HandleAsync([FromRoute] Guid securitykeyId, CancellationToken cancellationToken = default)
    {
        return new ActionResult<SecurityKeyResponse>(new SecurityKeyResponse(securitykeyId));
    }
}
