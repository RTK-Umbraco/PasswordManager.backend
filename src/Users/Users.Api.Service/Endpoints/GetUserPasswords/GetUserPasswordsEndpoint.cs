using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PasswordManager.Users.ApplicationServices.GetUserPasswords;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;
using PasswordManager.Users.Api.Service.CurrentUser;
using PasswordManager.Users.Api.Service.Models;
using PasswordManager.Users.Api.Service.Mappers;
using PasswordManager.Users.Domain.User;

namespace PasswordManager.Users.Api.Service.Endpoints.GetUserPasswords;

public class GetUserPasswordsEndpoint : EndpointBaseAsync.WithRequest<GetUserPasswordRequestWithBody>.WithActionResult<IEnumerable<UserPasswordResponse>>
{
    private readonly IGetUserPasswordsService _getUserPasswordsService;
    private readonly ICurrentUser _currentUser;

    public GetUserPasswordsEndpoint(IGetUserPasswordsService getUserPasswordsService, ICurrentUser _currentUser)
    {
        _getUserPasswordsService = getUserPasswordsService;
        this._currentUser = _currentUser;
    }

    [HttpPost("api/user/{userId:Guid}/passwords")]
    [ProducesResponseType(typeof(IEnumerable<UserPasswordResponse>), StatusCodes.Status200OK)]
    [SwaggerOperation(
        Summary = "Get user passwords by user id and url",
        Description = "Get user passwords by url",
        OperationId = "GetUserPasswordsByUrl",
        Tags = new[] { "Password" })
        ]
    [Authorize(AuthenticationSchemes = "FirebaseUser")]
    public override async Task<ActionResult<IEnumerable<UserPasswordResponse>>> HandleAsync([FromQuery] GetUserPasswordRequestWithBody request, CancellationToken cancellationToken = default)
    {
        IEnumerable<UserPasswordModel> userPasswordModel;

        if (request.Details.Url == null)
            userPasswordModel = await _getUserPasswordsService.GetUserPasswords(_currentUser.GetUser().Id);
        else
            userPasswordModel = await _getUserPasswordsService.GetUserPasswordsByUrl(_currentUser.GetUser().Id, request.Details.Url);

        var userPasswordResponse = userPasswordModel.Select(UserPasswordResponseMapper.Map);

        return Ok(userPasswordResponse);
    }
}

public sealed class GetUserPasswordRequestWithBody : UserRequest<GetUserPasswordRequestDetails> {}

[SwaggerSchema(Nullable = false, Required = new[] { "url" })]
public sealed class GetUserPasswordRequestDetails
{
    [JsonPropertyName("url")]
    public string Url { get; set; }
}
