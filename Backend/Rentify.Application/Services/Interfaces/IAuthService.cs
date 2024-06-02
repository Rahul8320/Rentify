using Rentify.Application.Models.DTOs;

namespace Rentify.Application.Services.Interfaces;

public interface IAuthService
{
    AuthenticatedUserDTO GetAuthenticatedUserData();
}
