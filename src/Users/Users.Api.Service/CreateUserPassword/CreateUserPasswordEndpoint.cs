using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using PasswordManager.Users.Api.Service.GetOperation;
using PasswordManager.Users.Api.Service.Models;
using PasswordManager.Users.ApplicationServices.CreateUserPassword;
using PasswordManager.Users.Domain.Operations;
using PasswordManager.Users.Domain.User;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;
using PasswordManager.Users.Api.Service.CurrentUser;

namespace PasswordManager.Users.Api.Service.CreateUserPassword;

public class CreateUserPasswordEndpoint : EndpointBaseAsync.WithRequest<CreateUserPasswordRequestWithBody>.WithoutResult
{
    private readonly ICreateUserPasswordService _createUserPasswordService;
    private readonly ICurrentUser _currentUser;


    public CreateUserPasswordEndpoint(ICreateUserPasswordService createUserPasswordService, ICurrentUser currentUser)
    {
        _createUserPasswordService = createUserPasswordService;
        _currentUser = currentUser;
    }

    [HttpPost("api/user/passwords")]
    [ProducesResponseType(typeof(OperationAcceptedResponse), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [SwaggerOperation(
    Summary = "Create a password for a user",
    Description = "Creates a user password",
    OperationId = "CreateUserPassword",
    Tags = new[] { "Password" })
]

    public override async Task<ActionResult> HandleAsync([FromRoute] CreateUserPasswordRequestWithBody request, CancellationToken cancellationToken = default)
    {
        var userPasswordModel = UserPasswordModel.CreateUserPassword(_currentUser.GetUser().Id, request.Details.Url, request.Details.FriendlyName, request.Details.Username, request.Details.Password);

        var operationResult = await _createUserPasswordService.RequestCreateUserPassword(userPasswordModel, new OperationDetails(request.CreatedByUserId));

        return operationResult.Status switch
        {
            OperationResultStatus.Accepted => new AcceptedResult(
                new Uri(GetOperationByRequestIdEndpoint.GetRelativePath(operationResult.GetOperation()), UriKind.Relative),
                new OperationAcceptedResponse(operationResult.GetOperation().RequestId)),

            OperationResultStatus.InvalidOperationRequest => Problem(title: "Cannot create user password", detail: operationResult.GetMessage(),
                statusCode: StatusCodes.Status400BadRequest),
            _ => Problem(title: "Unknown error requesting to create user password", detail: "Unknown error - check logs",
                statusCode: StatusCodes.Status500InternalServerError),
        };
    }
}

public sealed class CreateUserPasswordRequestWithBody : UserOperationRequest<CreateUserPasswordRequestDetails>
{
}

[SwaggerSchema(Nullable = false, Required = new[] { "url", "friendlyName", "username", "password" })]
public sealed class CreateUserPasswordRequestDetails
{
    [JsonPropertyName("url")]
    public string Url { get; set; }

    [JsonPropertyName("friendlyName")]
    public string FriendlyName { get; set; }

    [JsonPropertyName("username")]
    public string Username { get; set; }

    [JsonPropertyName("password")]
    public string Password { get; set; }
}