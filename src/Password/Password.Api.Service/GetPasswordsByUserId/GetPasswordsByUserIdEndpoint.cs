using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using PasswordManager.Password.Api.Service.GetPassword;
using PasswordManager.Password.Api.Service.Models;
using PasswordManager.Password.ApplicationServices.GetPassword;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PasswordManager.Password.Api.Service.GetPasswordsByUserId
{
    public sealed class GetPasswordsByUserIdEndpoint : EndpointBaseAsync.WithRequest<GetPasswordByUserIdRequestDetails>.WithActionResult<PasswordResponses>
    {
        private readonly IGetPasswordService _getPasswordService;

        public GetPasswordsByUserIdEndpoint(IGetPasswordService getPasswordService)
        {
            _getPasswordService = getPasswordService;
        }


        [HttpGet("api/passwords/user")]
        [ProducesResponseType(typeof(PasswordResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            Summary = "Gets Passwords from UserId",
            Description = "Gets Passwords from UserId",
            OperationId = "GetPasswordsFromUserId",
            Tags = new[] { "Password" })
        ]
        public override async Task<ActionResult<PasswordResponses>> HandleAsync([FromQuery] GetPasswordByUserIdRequestDetails request, CancellationToken cancellationToken = default)
        {
            var passwordModels = await _getPasswordService.GetPasswordsByUserId(request.UserId);

            return new ActionResult<PasswordResponses>(PasswordResponseMapper.Map(passwordModels));
        }
    }

    [SwaggerSchema(Nullable = false, Required = new[] { "userId" })]
    public sealed class GetPasswordByUserIdRequestDetails
    {
        [JsonPropertyName("userId")]
        public Guid UserId { get; set; }
    }
}
