using System.ComponentModel.DataAnnotations;
using Rentify.Domain.Enums;

namespace Rentify.Application.Models.Requests;

public class RegisterRequest
{
    [Required(ErrorMessage = "Fisrt name can not be empty!")]
    [MinLength(3, ErrorMessage = "Fisrt name must be 3 character long!")]
    [MaxLength(15, ErrorMessage = "Fisrt name can not be more than 15 character long.")]
    public string FisrtName { get; set; } = string.Empty;
    [Required(ErrorMessage = "Last name can not be empty!")]
    [MinLength(3, ErrorMessage = "Last name must be 3 character long!")]
    [MaxLength(15, ErrorMessage = "Last name can not be more than 15 character long.")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "UserName can not be empty!")]
    [MinLength(3, ErrorMessage = "UserName must be 3 character long!")]
    [MaxLength(15, ErrorMessage = "UserName can not be more than 15 character long.")]
    public string UserName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email can not be empty!")]
    [EmailAddress(ErrorMessage = "Please input an valid email address!")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Phone number can not be empty!")]
    [MinLength(10, ErrorMessage = "Phone number must be 10 character long!")]
    [MaxLength(10, ErrorMessage = "Phone number can not be more than 10 character long.")]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required]
    public UserRole UserRole { get; set; }

    [Required(ErrorMessage = "Password can not be empty!")]
    [MinLength(8, ErrorMessage = "Password must be 8 character long!")]
    [MaxLength(20, ErrorMessage = "Password can not be more than 20 character long.")]
    public string Password { get; set; } = string.Empty;
}
