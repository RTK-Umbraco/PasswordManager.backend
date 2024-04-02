﻿using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using PasswordManager.Users.ApplicationServices.GetUser;
using Swashbuckle.AspNetCore.Annotations;

namespace PasswordManager.Users.Api.Service.GetUser;

public class GetUserEndpoint : EndpointBaseAsync.WithRequest<Guid>.WithActionResult<UserResponse>
{
    private readonly IGetUserService _getUserService;
    public GetUserEndpoint(IGetUserService getUserService)
    {
        _getUserService = getUserService;
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
        var passwordModel = await _getUserService.GetUser(userId);

        if (passwordModel is null)
            return Problem(title: "User could not be found",
                           detail: $"User having id: '{userId}' not found",
                           statusCode: StatusCodes.Status404NotFound);

        return new ActionResult<UserResponse>(UserResponseMapper.Map(passwordModel));
    }
}