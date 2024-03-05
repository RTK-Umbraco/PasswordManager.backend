using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using PasswordManager.Password.Domain.Password;
using Swashbuckle.AspNetCore.Annotations;

namespace PasswordManager.Password.Api.Service.GetPassword;

public class GetPasswordEndpoint : EndpointBaseAsync.WithRequest<Guid>.WithActionResult<PasswordResponse>
{
    public GetPasswordEndpoint()
    {
    }

    [HttpGet("api/passwords/{passwordId:guid}")]
    [ProducesResponseType(typeof(PasswordResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [SwaggerOperation(
        Summary = "Get Password by Password id",
        Description = "Get Password by Password id",
        OperationId = "GetPassword",
        Tags = new[] { "Password" })
    ]
    public override async Task<ActionResult<PasswordResponse>> HandleAsync([FromRoute] Guid passwordId, CancellationToken cancellationToken = default)
    {
        var passwordResponse = new PasswordModel(passwordId, DateTime.UtcNow, DateTime.UtcNow, false, "url", "friendlyName", "username", "password");
        return new ActionResult<PasswordResponse>(PasswordResponseMapper.Map(passwordResponse));
    }
}
