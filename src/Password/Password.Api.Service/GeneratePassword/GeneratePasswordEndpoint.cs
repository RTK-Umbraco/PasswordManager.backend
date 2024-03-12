﻿using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using PasswordManager.Password.Api.Service.GetPassword;
using PasswordManager.Password.Api.Service.Models;
using PasswordManager.Password.ApplicationServices.PasswordGenerator;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PasswordManager.Password.Api.Service.GeneratePassword
{
    public sealed class GeneratePasswordEndpoint : EndpointBaseAsync.WithRequest<CreatePasswordRequestWithBody>.WithActionResult<GeneratePasswordResponse>
    {
        private readonly IGeneratePasswordService _generatePasswordService;

        public GeneratePasswordEndpoint(IGeneratePasswordService generatePasswordService)
        {
            _generatePasswordService = generatePasswordService;
        }

        [HttpGet("api/generate/password")]
        [ProducesResponseType(typeof(GeneratePasswordResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [SwaggerOperation(
        Summary = "Generates Password",
        Description = "Generates a password",
        OperationId = "GeneratePassword",
        Tags = new[] { "Password" })
        ]

        public override async Task<ActionResult<GeneratePasswordResponse>> HandleAsync([FromBody] CreatePasswordRequestWithBody request, CancellationToken cancellationToken)
        {
            var password = new GeneratePasswordResponse(await _generatePasswordService.GeneratePassword(request.Details.Length));

            return Ok(password);
        }
    }

    public sealed class  CreatePasswordRequestWithBody : UserOperationRequest<GeneratePasswordRequestDetails>
    {
    }

    [SwaggerSchema(Nullable = false, Required = new[] { "length" })]
    public sealed class GeneratePasswordRequestDetails
    {
        [JsonPropertyName("length")]
        public int Length { get; set; }
    }
}
