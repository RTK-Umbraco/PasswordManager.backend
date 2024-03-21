using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace PasswordManager.Users.Api.Service.Models;

public class UserRequest<T> 
{
    [FromRoute(Name = "userId")]
    [Required]
    public Guid UserId { get; set; }
    [FromBody] public T Details { get; set; }
}
