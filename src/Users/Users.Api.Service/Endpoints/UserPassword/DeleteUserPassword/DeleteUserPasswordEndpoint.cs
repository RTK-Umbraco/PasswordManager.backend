using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using PasswordManager.Users.Api.Service.CurrentUser;
using PasswordManager.Users.Domain.Operations;
using Swashbuckle.AspNetCore.Annotations;

namespace PasswordManager.Users.Api.Service.Endpoints.UserPassword.DeleteUserPassword
{
    public class DeleteUserPasswordEndpoint : EndpointBaseAsync.WithRequest<Guid>.WithoutResult
    {
        private readonly IDeleteUserPasswordService _deleteUserPasswordService;
        private readonly ICurrentUser _currentUser;

        public DeleteUserPasswordEndpoint(IDeleteUserPasswordService deleteUserPasswordService, ICurrentUser currentUser)
        {
            _deleteUserPasswordService = deleteUserPasswordService;
            _currentUser = currentUser;
        }

        [HttpDelete("api/user/passwords/{passwordId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
                       Summary = "Delete a password for a user",
                       Description = "Deletes a user password",
                       OperationId = "DeleteUserPassword",
                       Tags = new[] { "Password" })
                   ]
        public async Task<ActionResult> HandleAsync([FromRoute] Guid passwordId, CancellationToken cancellationToken = default)
        {
            var operationResult = await _deleteUserPasswordService.RequestDeleteUserPassword(passwordId, new OperationDetails(_currentUser.GetUser().Id));

            return operationResult.Status switch
            {
                OperationResultStatus.Accepted => new NoContentResult(),

                OperationResultStatus.InvalidOperationRequest => Problem(title: "Cannot delete user password", detail: operationResult.GetMessage(),
                                   statusCode: StatusCodes.Status400BadRequest),
                _ => Problem(title: "Unknown error requesting to delete user password", detail: "Unknown error - check logs",
                                   statusCode: StatusCodes.Status500InternalServerError),
            };
        }
    }
}
