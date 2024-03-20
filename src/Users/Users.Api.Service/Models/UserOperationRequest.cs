using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace PasswordManager.Users.Api.Service.Models;

public abstract class UserOperationRequest<T> : OperationRequest
{
    [FromRoute(Name = "userId")]
    [Required]
    public Guid UserId { get; set; }
    [FromBody] public T Details { get; set; }
}

