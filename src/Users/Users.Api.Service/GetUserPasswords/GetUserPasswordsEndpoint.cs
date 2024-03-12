using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using PasswordManager.Users.ApplicationServices.GetUserPasswords;
using Swashbuckle.AspNetCore.Annotations;

namespace PasswordManager.Users.Api.Service.GetUserPasswords;

public class GetUserPasswordsEndpoint : EndpointBaseAsync.WithRequest<Guid>.WithActionResult<IEnumerable<UserPasswordResponse>>
{
    private readonly IGetUserPasswordsService _getUserPasswordsService;
    public GetUserPasswordsEndpoint(IGetUserPasswordsService getUserPasswordsService)
    {
        _getUserPasswordsService = getUserPasswordsService;
    }

    [HttpGet("api/users/{userId}/passwords")]
    [ProducesResponseType(typeof(UserPasswordResponse), StatusCodes.Status200OK)]
    [SwaggerOperation(
        Summary = "Get user passwords by user id",
        Description = "Get user passwords",
        OperationId = "GetUserPasswords",
        Tags = new[] { "User" })
        ]
    public override async Task<ActionResult<IEnumerable<UserPasswordResponse>>> HandleAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var userPasswordModel = await _getUserPasswordsService.GetUserPasswords(userId);

        return new ActionResult<IEnumerable<UserPasswordResponse>>(userPasswordModel.Select(UserPasswordResponseMapper.Map));
    }
}
