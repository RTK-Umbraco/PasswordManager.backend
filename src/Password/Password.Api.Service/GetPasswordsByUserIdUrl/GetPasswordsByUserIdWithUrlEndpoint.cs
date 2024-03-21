using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using PasswordManager.Password.Api.Service.GetPassword;
using PasswordManager.Password.Api.Service.Models;
using PasswordManager.Password.ApplicationServices.GetPassword;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace PasswordManager.Password.Api.Service.GetPasswordsByUserUrl
{
    public sealed class GetPasswordsByUserIdWithUrlEndpoint : EndpointBaseAsync.WithRequest<GetPasswordByUserIdWithUrlRequestDetails>.WithActionResult<PasswordResponses>
    {
        private readonly IGetPasswordService _getPasswordService;

        public GetPasswordsByUserIdWithUrlEndpoint(IGetPasswordService getPasswordService)
        {
            _getPasswordService = getPasswordService;
        }


        [HttpGet("api/passwords/users/url")]
        [ProducesResponseType(typeof(PasswordResponses), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            Summary = "Gets Passwords from UserId with Url",
            Description = "Gets Passwords from the UserId with the specified url",
            OperationId = "GetPasswordsFromUserIdWithUrl",
            Tags = new[] { "Password" })
        ]
        public override async Task<ActionResult<PasswordResponses>> HandleAsync([FromBody] GetPasswordByUserIdWithUrlRequestDetails request, CancellationToken cancellationToken = default)
        {
            var passwordModels = await _getPasswordService.GetPasswordsByUserIdWithUrl(request.UserId, request.Url);

            return new ActionResult<PasswordResponses>(PasswordResponseMapper.Map(passwordModels));
        }
    }

    [SwaggerSchema(Nullable = false, Required = new[] { "userId", "url" })]
    public sealed class GetPasswordByUserIdWithUrlRequestDetails
    {
        [JsonPropertyName("userId")]
        public Guid UserId { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}
