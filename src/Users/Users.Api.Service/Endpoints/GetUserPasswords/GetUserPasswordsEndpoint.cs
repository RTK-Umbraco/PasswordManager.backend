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
using System.ComponentModel.DataAnnotations;

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

    [HttpGet("api/user/{userId:Guid}/passwords")]
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

        if (request.Url == null)
            userPasswordModel = await _getUserPasswordsService.GetUserPasswords(_currentUser.GetUser().Id);
        else
            userPasswordModel = await _getUserPasswordsService.GetUserPasswordsByUrl(_currentUser.GetUser().Id, request.Url);

        var userPasswordResponse = userPasswordModel.Select(UserPasswordResponseMapper.Map);

        return Ok(userPasswordResponse);
    }
}

public sealed class GetUserPasswordRequestWithBody 
{
    [FromRoute(Name = "userId")]
    [Required]
    public Guid UserId { get; set; }

    [FromQuery]
    public string? Url { get; set; }
}
