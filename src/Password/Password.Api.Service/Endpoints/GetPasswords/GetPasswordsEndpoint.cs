using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using PasswordManager.Password.Api.Service.Endpoints.GetPassword;
using PasswordManager.Password.ApplicationServices.GetPassword;
using Swashbuckle.AspNetCore.Annotations;

namespace PasswordManager.Password.Api.Service.Endpoints.GetPasswords;

public sealed class GetPasswordsEndpoint : EndpointBaseAsync.WithoutRequest.WithActionResult<IEnumerable<PasswordResponse>>
{
    private readonly IGetPasswordService _getPasswordService;

    public GetPasswordsEndpoint(IGetPasswordService getPasswordService)
    {
        _getPasswordService = getPasswordService;
    }


    [HttpGet("api/passwords")]
    [ProducesResponseType(typeof(PasswordResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [SwaggerOperation(
        Summary = "Gets Passwords",
        Description = "Gets Passwords",
        OperationId = "GetPasswords",
        Tags = new[] { "Password" })
    ]
    public override async Task<ActionResult<IEnumerable<PasswordResponse>>> HandleAsync(CancellationToken cancellationToken = default)
    {
        var passwordModels = await _getPasswordService.GetPasswords();

        return new ActionResult<IEnumerable<PasswordResponse>>(PasswordResponseMapper.Map(passwordModels));
    }
}
