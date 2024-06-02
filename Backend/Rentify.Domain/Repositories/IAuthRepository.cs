using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using Rentify.Domain.Entities;
using Rentify.Domain.Enums;

namespace Rentify.Domain.Repositories;

public interface IAuthRepository
{
    public Task<ApplicationUser?> GetUserByEmail(string email);
    public Task<ApplicationUser?> GetUserById(Guid userId);
    public Task<ApplicationUser?> GetUserByUserName(string username);
    public Task<IdentityResult> CreateUser(ApplicationUser applicationUser, UserRole userRole, string password);
    public Task<JwtSecurityToken> CreateLoginToken(ApplicationUser applicationUser);
    public Task<bool> IsValidPassword(ApplicationUser applicationUser, string password);
}
