using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace PasswordManager.Users.Api.Service.Models;

public abstract class UserOperationRequest<T> : OperationRequest
{
    [FromBody] public T Details { get; set; }
}

