using System.Net;
using MediatR;
using Rentify.Application.Models.Responses;
using Rentify.Application.Services;
using Rentify.Application.Services.Interfaces;
using Rentify.Domain.Repositories;
using Rentify.Domain.Services;

namespace Rentify.Application.Commands.Handlers;

public class LikePropertyHandler(
    ILoggerService logger,
    IAuthService authService,
    ICachingService cachingService,
    IPropertyRepository propertyRepository) : IRequestHandler<LikeProperty, RequestResponse<PropertyResponse>>
{
    public async Task<RequestResponse<PropertyResponse>> Handle(LikeProperty request, CancellationToken cancellationToken)
    {
        try
        {
            var serviceResult = new RequestResponse<PropertyResponse>();
            var userData = authService.GetAuthenticatedUserData();

            logger.LogInformation(message: "Fetching propertry...");

            var existingProperty = await propertyRepository.GetById(request.PropertyId, cancellationToken);

            if (existingProperty == null)
            {
                logger.LogWarning(message: $"Property not found! Id: {request.PropertyId}");
                serviceResult.StatusCode = HttpStatusCode.NotFound;
                serviceResult.Message = $"Property with id: {request.PropertyId} not found in database!";
                return serviceResult;
            }

            logger.LogInformation(message: "Adding like into propertry...");

            existingProperty.Likes.Add(userData.UserId);
            existingProperty.LastUpdated = DateTime.UtcNow;

            var updatedProperty = await propertyRepository.Update(existingProperty, cancellationToken);

            if (updatedProperty == false)
            {
                logger.LogError($"Failed to add like into property! PropertyId: {existingProperty.Id}, UserID: {userData.UserId}.");
                serviceResult.StatusCode = HttpStatusCode.BadRequest;
                serviceResult.Message = "Failed to like this property.";
                return serviceResult;
            }

            logger.LogInformation($"Successfully liked this property! PropertyId: {existingProperty.Id}, UserID: {userData.UserId}.");

            // Removed cached for all 
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
