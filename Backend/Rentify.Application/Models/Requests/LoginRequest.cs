using System.ComponentModel.DataAnnotations;

namespace Rentify.Application.Models.Requests;

public class LoginRequest
{
    [Required(ErrorMessage = "Username can not be empty!")]
    public string UserName { get; set; } = default!;

    [Required(ErrorMessage = "Password can not be empty!")]
    public string Password { get; set; } = default!;
}
