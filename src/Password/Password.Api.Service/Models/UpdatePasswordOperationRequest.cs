using Microsoft.AspNetCore.Mvc;

namespace PasswordManager.Password.Api.Service.Models;

public class UpdatePasswordOperationRequest<T> : OperationRequest
{
    [FromRoute(Name = "userId")]
    public Guid UserId { get; set; }

    [FromBody] public T Details { get; set; }
}

