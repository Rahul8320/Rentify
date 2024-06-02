using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Rentify.Application.Models.DTOs;
using Rentify.Application.Services.Interfaces;
using Rentify.Domain.Services;

namespace Rentify.Application.Services;

public class AuthService(IHttpContextAccessor httpContext, ILoggerService logger) : IAuthService
{
    public AuthenticatedUserDTO GetAuthenticatedUserData()
    {
        try
        {
            // initialize auth user dto
            AuthenticatedUserDTO authenticatedUserDTO = new();

            // Fetch claims identity from http context
            var claimsIdentity = httpContext.HttpContext?.User?.Identities.FirstOrDefault(item => item.Claims.Any());
            var claims = claimsIdentity?.Claims;

            // Get claims value
            var userId = claims?.FirstOrDefault(item => item.Type == ClaimTypes.GivenName)?.Value;
            authenticatedUserDTO.UserId = Guid.Empty;

            if (userId != null)
            {
                authenticatedUserDTO.UserId = Guid.Parse(userId);
            }

            authenticatedUserDTO.Email = claims?.FirstOrDefault(Item => Item.Type == ClaimTypes.Email)?.Value;
            authenticatedUserDTO.UserName = claims?.FirstOrDefault(item => item.Type == ClaimTypes.Name)?.Value;
            authenticatedUserDTO.Role = claims?.FirstOrDefault(item => item.Type == ClaimTypes.Role)?.Value;

            // check for user details is exists or not
            if (authenticatedUserDTO.UserId == Guid.Empty ||
                authenticatedUserDTO.Role == null ||
                authenticatedUserDTO.UserName == null ||
                authenticatedUserDTO.Email == null)
            {
                throw new Exception("Does not get user credentials!");
            }

            // return the auth user data.
            return authenticatedUserDTO;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            throw;
        }
    }
}
