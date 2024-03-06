using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using PasswordManager.Password.Api.Service.GetOperation;
using PasswordManager.Password.ApplicationServices.AddPassword;
using PasswordManager.Password.Domain.Operations;
using PasswordManager.Password.Domain.Password;
using Swashbuckle.AspNetCore.Annotations;

namespace PasswordManager.Password.Api.Service.CreatePassword
{
    public class CreatePasswordEndpoint : EndpointBaseAsync.WithRequest<CreatePasswordRequestWithBody>.WithoutResult
    {
        private readonly ICreatePasswordService _createPasswordService;

        public CreatePasswordEndpoint(ICreatePasswordService createPasswordService)
        {
            _createPasswordService = createPasswordService;
        }

        [HttpPost("api/password")]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
        Summary = "Create password",
        Description = "Creates password",
        OperationId = "CreatePassword",
        Tags = new[] { "Password" })
    ]
        public override async Task<ActionResult> HandleAsync([FromBody] CreatePasswordRequestWithBody request, CancellationToken cancellationToken = default)
        {
            var passwordModel = PasswordModel.CreatePassword(request.Url, request.Label, request.Username, request.Key);
            
            var operationResult = await _createPasswordService.RequestCreatePassword(passwordModel, new OperationDetails("CreatePasswordEndpoint"));

            return operationResult.Status switch
            {
                OperationResultStatus.Accepted => Ok(),

                OperationResultStatus.InvalidOperationRequest => Problem(title: "Cannot create password", detail: operationResult.GetMessage(),
                    statusCode: StatusCodes.Status400BadRequest),
                _ => Problem(title: "Unknown error requesting to creating password", detail: "Unknown error - check logs",
                    statusCode: StatusCodes.Status500InternalServerError),
            };
        }
    }

    public record CreatePasswordRequestWithBody(string Url, string Label, string Username, string Key);
}
