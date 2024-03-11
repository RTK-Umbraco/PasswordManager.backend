namespace PasswordManager.Password.Api.Service.Models;

public abstract class OperationRequest
{
    [FromHeader(Name = "created-by-user-id")][Required] public string CreatedByUserId { get; set; }
}
