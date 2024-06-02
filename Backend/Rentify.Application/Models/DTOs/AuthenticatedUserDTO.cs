namespace Rentify.Application.Models.DTOs;

public class AuthenticatedUserDTO
{
    /// <summary>
    /// Gets or sets user name
    /// </summary>
    public string? UserName { get; set; }
    /// <summary>
    /// Gets or sets Email
    /// </summary>
    public string? Email { get; set; }
    /// <summary>
    /// Gets or sets user id
    /// </summary>
    public Guid UserId { get; set; }
    /// <summary>
    /// Gets or sets role
    /// </summary>
    public string? Role { get; set; }
}
