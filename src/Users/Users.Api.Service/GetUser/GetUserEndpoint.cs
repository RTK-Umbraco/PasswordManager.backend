using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace PasswordManager.Users.Api.Service.GetUser;

public class GetUserEndpoint : EndpointBaseAsync.WithRequest<Guid>.WithActionResult<UserResponse>
{
    public GetUserEndpoint()
    {
    }

    [HttpGet("api/users/{userId:guid}")]
    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [SwaggerOperation(
        Summary = "Get User by User id",
        Description = "Get User by User id",
        OperationId = "GetUser",
        Tags = new[] { "User" })
    ]
    public override async Task<ActionResult<UserResponse>> HandleAsync([FromRoute] Guid userId, CancellationToken cancellationToken = default)
    {
        return new ActionResult<UserResponse>(new UserResponse(userId));
    }
}
