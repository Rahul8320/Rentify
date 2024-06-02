using System.IdentityModel.Tokens.Jwt;
using System.Net;
using MediatR;
using Rentify.Application.Models.Responses;
using Rentify.Domain.Repositories;
using Rentify.Domain.Services;

namespace Rentify.Application.Commands.Handlers;

public class LoiginCommandHandler(
    IAuthRepository authRepository,
    ILoggerService logger) : IRequestHandler<LoginCommand, RequestResponse<JwtSecurityToken>>
{
    public async Task<RequestResponse<JwtSecurityToken>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var servcieResult = new RequestResponse<JwtSecurityToken>();

            // Fetch user data.
            var existingUser = await authRepository.GetUserByUserName(request.LoginRequest.UserName);

            if (existingUser == null)
            {
                servcieResult.StatusCode = HttpStatusCode.Unauthorized;
                servcieResult.Message = "Invalid credentials";
                return servcieResult;
            }

            // check the password is valid
            var result = await authRepository.IsValidPassword(existingUser, request.LoginRequest.Password);

            if (result == false)
            {
                servcieResult.StatusCode = HttpStatusCode.Unauthorized;
                servcieResult.Message = "Invalid credentials";
                return servcieResult;
            }

            // get jwt token
            var jwtToken = await authRepository.CreateLoginToken(existingUser);

            servcieResult.Data = jwtToken;

            return servcieResult;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            throw;
        }
    }
}
