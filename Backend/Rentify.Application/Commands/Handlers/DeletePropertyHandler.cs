using System.Net;
using MediatR;
using Rentify.Application.Models.Responses;
using Rentify.Application.Services;
using Rentify.Application.Services.Interfaces;
using Rentify.Domain.Enums;
using Rentify.Domain.Repositories;
using Rentify.Domain.Services;

namespace Rentify.Application.Commands.Handlers;

public class DeletePropertyHandler(
    IPropertyRepository propertyRepository,
    IAuthService authService,
    ICachingService cachingService,
    ILoggerService logger) : IRequestHandler<DeleteProperty, RequestResponse>
{
    public async Task<RequestResponse> Handle(DeleteProperty request, CancellationToken cancellationToken)
    {
        try
        {
            var serviceResult = new RequestResponse();

            // Get logged in user data
            var userData = authService.GetAuthenticatedUserData();

            if (UserRole.Seller.ToString().Equals(userData.Role, StringComparison.OrdinalIgnoreCase) == false)
            {
                serviceResult.StatusCode = HttpStatusCode.Unauthorized;
                serviceResult.Message = "Not authorized to do this operation!";
                logger.LogWarning($"Unauthorized update request! UserID: {userData.UserId}, UserRole: {userData.Role}, Name: {userData.UserName}.");
                return serviceResult;
            }

            logger.LogInformation(message: "Fetching propertry...");

            var existingProperty = await propertyRepository.GetById(request.PropertyId, cancellationToken);

            if (existingProperty == null)
            {
                logger.LogWarning(message: $"Property not found! Id: {request.PropertyId}");
                serviceResult.StatusCode = HttpStatusCode.NotFound;
                serviceResult.Message = $"Property with id: {request.PropertyId} not found in database!";
                return serviceResult;
            }

            logger.LogInformation(message: "Deleting propertry...");

            if (existingProperty.Owner != userData.UserId)
            {
                serviceResult.StatusCode = HttpStatusCode.Unauthorized;
                serviceResult.Message = "Not authorized to do this operation!";
                logger.LogWarning($"Unauthorized update request! UserID: {userData.UserId}, UserRole: {userData.Role}, Name: {userData.UserName}.");
                return serviceResult;
            }

            existingProperty.Status = 1;
            existingProperty.LastUpdated = DateTime.UtcNow;

            var updatedProperty = await propertyRepository.Update(existingProperty, cancellationToken);

            if (updatedProperty == false)
            {
                logger.LogError($"Failed to delete property! PropertyId: {existingProperty.Id}, UserID: {userData.UserId}, UserRole: {userData.Role}, Name: {userData.UserName}.");
                serviceResult.StatusCode = HttpStatusCode.BadRequest;
                serviceResult.Message = "Failed to delete property.";
                return serviceResult;
            }

            logger.LogInformation($"Successfully delete property! PropertyId: {existingProperty.Id}, UserID: {userData.UserId}, UserRole: {userData.Role}, Name: {userData.UserName}.");

            // Removed cached for all property
            cachingService.RemoveData(CachingKeys.AllPropertyKey);
            cachingService.RemoveData(CachingKeys.PropertyDetailsKey);

            return serviceResult;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            throw;
        }
    }
}
