using Microsoft.AspNetCore.Mvc;

namespace PasswordManager.Password.Api.Service.Models;

public class UpdatePasswordOperationRequest<T> : OperationRequest
{
    [FromRoute(Name = "passwordId")]
    public Guid PasswordId { get; set; }

    [FromBody] public T Details { get; set; }
}