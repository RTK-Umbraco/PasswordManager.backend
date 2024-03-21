using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using PasswordManager.Users.Api.Service.Models;
using PasswordManager.Users.ApplicationServices.GetUserPasswords;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PasswordManager.Users.Api.Service.GetUserPasswords;

public class GetUserPasswordsEndpoint : EndpointBaseAsync.WithRequest<GetUserPasswordRequestWithBody>.WithActionResult<IEnumerable<UserPasswordResponse>>
{
    private readonly IGetUserPasswordsService _getUserPasswordsService;
    public GetUserPasswordsEndpoint(IGetUserPasswordsService getUserPasswordsService)
    {
        _getUserPasswordsService = getUserPasswordsService;
    }

    [HttpGet("api/users/{userId}/passwords")]
    [ProducesResponseType(typeof(UserPasswordResponse), StatusCodes.Status200OK)]
    [SwaggerOperation(
        Summary = "Get user passwords by user id and url",
        Description = "Get user passwords by url",
        OperationId = "GetUserPasswords",
        Tags = new[] { "Password" })
        ]

    public override async Task<ActionResult<IEnumerable<UserPasswordResponse>>> HandleAsync([FromRoute] GetUserPasswordRequestWithBody request, CancellationToken cancellationToken = default)
    {
        var userPasswordModel = await _getUserPasswordsService.GetUserPasswordsByUrl(request.UserId, request.Details.Url);

        return new ActionResult<IEnumerable<UserPasswordResponse>>(userPasswordModel.Select(UserPasswordResponseMapper.Map));
    }
}

public sealed class GetUserPasswordRequestWithBody : UserRequest<GetUserPasswordRequestDetails> {}

[SwaggerSchema(Nullable = false, Required = new[] { "url" })]
public sealed class GetUserPasswordRequestDetails
{
    [JsonPropertyName("url")]
    public string Url { get; set; }
}
