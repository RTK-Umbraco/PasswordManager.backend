using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using PasswordManager.Password.Api.Service.GetPassword;
using PasswordManager.Password.ApplicationServices.PasswordGenerator;
using Swashbuckle.AspNetCore.Annotations;

namespace PasswordManager.Password.Api.Service.GeneratePassword
{
    public sealed class GeneratePasswordEndpoint : EndpointBaseAsync.WithoutRequest.WithActionResult<GeneratePasswordResponse>
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

        public override async Task<ActionResult<GeneratePasswordResponse>> HandleAsync(CancellationToken cancellationToken)
        {
            var password = new GeneratePasswordResponse(await _generatePasswordService.GeneratePassword(20));

            return Ok(password);
        }
    }
}
