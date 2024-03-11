namespace PasswordManager.Password.Api.Service.Models;

public abstract class UserOperationRequest<T> : OperationRequest
{
    [FromBody] public T Details { get; set; }
}

