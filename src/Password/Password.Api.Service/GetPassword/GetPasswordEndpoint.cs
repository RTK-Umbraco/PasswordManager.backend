using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using PasswordManager.Password.ApplicationServices.GetPassword;
using PasswordManager.Password.Domain.Password;
using Swashbuckle.AspNetCore.Annotations;

namespace PasswordManager.Password.Api.Service.GetPassword;

public class GetPasswordEndpoint : EndpointBaseAsync.WithRequest<Guid>.WithActionResult<PasswordResponse>
{
    private readonly IGetPasswordService _getPasswordService;

    public GetPasswordEndpoint(IGetPasswordService getPasswordService)
    {
        _getPasswordService = getPasswordService;
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
        var passwordModel = await _getPasswordService.GetPassword(passwordId);

        if (passwordModel is null)
            return Problem(title: "Password could not be found",
                           detail: $"Password having id: '{passwordId}' not found", 
                           statusCode: StatusCodes.Status404NotFound);

        return new ActionResult<PasswordResponse>(PasswordResponseMapper.Map(passwordModel));
    }
}
