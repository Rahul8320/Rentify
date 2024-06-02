using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Rentify.Domain.Entities;
using Rentify.Domain.Enums;
using Rentify.Domain.Repositories;
using Rentify.Domain.Services;

namespace Rentity.Infrastructure.Repositories;

public class AuthRepository(
    UserManager<ApplicationUser> userManager,
    RoleManager<IdentityRole> roleManager,
    ILoggerService logger,
    IConfiguration configuration) : IAuthRepository
{
    public async Task<JwtSecurityToken> CreateLoginToken(ApplicationUser applicationUser)
    {
        try
        {
            // create claim list
            var authClaims = new List<Claim>
                {
                    new(ClaimTypes.Name, applicationUser.UserName!),
                    new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new(ClaimTypes.Email, applicationUser.Email!),
                    new(ClaimTypes.GivenName, applicationUser.Id)
                };

            // get user role
            var userRoles = await userManager.GetRolesAsync(applicationUser);

            foreach (var role in userRoles)
            {
                authClaims.Add(new(ClaimTypes.Role, role));
            }

            // generate jwt token
            var jwtToken = GetToken(authClaims);

            return jwtToken;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            throw;
        }
    }

    public async Task<IdentityResult> CreateUser(ApplicationUser applicationUser, UserRole userRole, string password)
    {
        try
        {
            // Create new user
            var result = await userManager.CreateAsync(applicationUser, password);

            if (!result.Succeeded)
            {
                return result;
            }

            // Assign User Role to user
            if (await roleManager.RoleExistsAsync(userRole.ToString()))
            {
                await userManager.AddToRoleAsync(applicationUser, userRole.ToString());
            }

            return result;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            throw;
        }
    }

    public async Task<ApplicationUser?> GetUserById(Guid userId)
    {
        try
        {
            return await userManager.FindByIdAsync(userId.ToString());
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            throw;
        }
    }

    public async Task<ApplicationUser?> GetUserByEmail(string email)
    {
        try
        {
            return await userManager.FindByEmailAsync(email);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            throw;
        }
    }

    public async Task<ApplicationUser?> GetUserByUserName(string username)
    {
        try
        {
            return await userManager.FindByNameAsync(username);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            throw;
        }
    }

    public async Task<bool> IsValidPassword(ApplicationUser applicationUser, string password)
    {
        try
        {
            // Check for password is match or not
            return await userManager.CheckPasswordAsync(applicationUser, password);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            throw;
        }
    }

    // Get Jwt Token
    private JwtSecurityToken GetToken(List<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTConfigs:Secret"]!));

        var token = new JwtSecurityToken(
            issuer: configuration["JWTConfigs:ValidIssuer"],
            audience: configuration["JWTConfigs:ValidAudience"],
            expires: DateTime.Now.AddHours(1),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        return token;
    }
}
