using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PasswordManager.Users.ApplicationServices.GetUserPasswords;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;
using PasswordManager.Users.Api.Service.CurrentUser;
using PasswordManager.Users.Api.Service.Models;

namespace PasswordManager.Users.Api.Service.GetUserPasswords;

public class GetUserPasswordsEndpoint : EndpointBaseAsync.WithRequest<GetUserPasswordRequestWithBody>.WithActionResult<IEnumerable<UserPasswordResponse>>
{
    private readonly IGetUserPasswordsService _getUserPasswordsService;
    private readonly ICurrentUser _currentUser;

    public GetUserPasswordsEndpoint(IGetUserPasswordsService getUserPasswordsService, ICurrentUser _currentUser)
    {
        _getUserPasswordsService = getUserPasswordsService;
        this._currentUser = _currentUser;
    }

    [HttpPost("api/user/passwords/url")]
    [ProducesResponseType(typeof(IEnumerable<UserPasswordResponse>), StatusCodes.Status200OK)]
    [SwaggerOperation(
        Summary = "Get user passwords by user id and url",
        Description = "Get user passwords by url",
        OperationId = "GetUserPasswordsByUrl",
        Tags = new[] { "Password" })
        ]
    [Authorize(AuthenticationSchemes = "FirebaseUser")]
    public override async Task<ActionResult<IEnumerable<UserPasswordResponse>>> HandleAsync([FromBody] GetUserPasswordRequestWithBody request, CancellationToken cancellationToken = default)
    {
        var userPasswordModel = await _getUserPasswordsService.GetUserPasswordsByUrl(_currentUser.GetUser().Id, request.Details.Url);

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
