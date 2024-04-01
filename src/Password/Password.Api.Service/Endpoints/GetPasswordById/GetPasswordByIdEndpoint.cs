﻿using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using PasswordManager.Password.Api.Service.Mappers;
using PasswordManager.Password.Api.Service.Models;
using PasswordManager.Password.ApplicationServices.Password.GetPassword;
using Swashbuckle.AspNetCore.Annotations;

<<<<<<<< HEAD:src/Password/Password.Api.Service/Endpoints/GetPasswordById/GetPasswordByIdEndpoint.cs
namespace PasswordManager.Password.Api.Service.Endpoints.GetPasswordById;
========
namespace PasswordManager.Password.Api.Service.GetPassword;
>>>>>>>> feature/davu/firebase-user:src/Password/Password.Api.Service/GetPassword/GetPasswordEndpoint.cs

public class GetPasswordByIdEndpoint : EndpointBaseAsync.WithRequest<Guid>.WithActionResult<PasswordResponse>
{
    private readonly IGetPasswordService _getPasswordService;

    public GetPasswordByIdEndpoint(IGetPasswordService getPasswordService)
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

        var passwordResponse = PasswordResponseMapper.Map(passwordModel);

        return Ok(passwordResponse);
    }
}