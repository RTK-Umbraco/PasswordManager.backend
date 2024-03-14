using Microsoft.AspNetCore.Mvc;

namespace PasswordManager.KeyVaults.Api.Service.Models;

public class SecurityKeyOperationRequest<T> : OperationRequest
{
    [FromBody] public T Details { get; set; }
}
