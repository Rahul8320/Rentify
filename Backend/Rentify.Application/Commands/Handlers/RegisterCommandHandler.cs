using System.Net;
using MediatR;
using Rentify.Application.Models.Responses;
using Rentify.Domain.Entities;
using Rentify.Domain.Repositories;
using Rentify.Domain.Services;

namespace Rentify.Application.Commands.Handlers;

public class RegisterCommandHandler(
    IAuthRepository authRepository,
    ILoggerService logger) : IRequestHandler<RegisterCommand, RequestResponse>
{
    public async Task<RequestResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var servcieResult = new RequestResponse();

            // Fetch user data.
            var existingUser = await authRepository.GetUserByEmail(request.RegisterRequest.Email);

            if (existingUser != null)
            {
                servcieResult.StatusCode = HttpStatusCode.Conflict;
                servcieResult.Message = "User Already Exists!";
                return servcieResult;
            }

            // Create new application User
            ApplicationUser user = new()
            {
                Id = Guid.NewGuid().ToString(),
                Email = request.RegisterRequest.Email,
                UserName = request.RegisterRequest.UserName,
                SecurityStamp = Guid.NewGuid().ToString(),
                FirstName = request.RegisterRequest.FisrtName,
                LastName = request.RegisterRequest.LastName,
                PhoneNumber = request.RegisterRequest.PhoneNumber,
            };

            // Create New User in database
            var result = await authRepository.CreateUser(user, request.RegisterRequest.UserRole, request.RegisterRequest.Password);

            // check for success result.
            if (result.Succeeded == false)
            {
                servcieResult.StatusCode = HttpStatusCode.BadRequest;
                var error = result.Errors.FirstOrDefault();

                if (error == null)
                {
                    servcieResult.Message = "User Register Failed!";
                    return servcieResult;
                }

                servcieResult.Message = error.Description;
                return servcieResult;
            }

            servcieResult.Message = "User Register Successfully.";

            return servcieResult;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            throw;
        }
    }
}
