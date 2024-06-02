using System.Net;
using MediatR;
using Rentify.Application.Models.Responses;
using Rentify.Application.Services;
using Rentify.Application.Services.Interfaces;
using Rentify.Domain.Enums;
using Rentify.Domain.Repositories;
using Rentify.Domain.Services;

namespace Rentify.Application.Commands.Handlers;

public class UpdatePropertyHandler(
    IPropertyRepository propertyRepository,
    IAuthService authService,
    ICachingService cachingService,
    ILoggerService logger) : IRequestHandler<UpdateProperty, RequestResponse>
{
    public async Task<RequestResponse> Handle(UpdateProperty request, CancellationToken cancellationToken)
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

            var existingProperty = await propertyRepository.GetById(request.PropertyRequest.Id, cancellationToken);

            if (existingProperty == null)
            {
                logger.LogWarning(message: $"Property not found! Id: {request.PropertyRequest.Id}");
                serviceResult.StatusCode = HttpStatusCode.NotFound;
                serviceResult.Message = $"Property with id: {request.PropertyRequest.Id} not found in database!";
                return serviceResult;
            }

            logger.LogInformation(message: "Updating propertry...");

            if (existingProperty.Owner != userData.UserId)
            {
                serviceResult.StatusCode = HttpStatusCode.Unauthorized;
                serviceResult.Message = "Not authorized to do this operation!";
                logger.LogWarning($"Unauthorized update request! UserID: {userData.UserId}, UserRole: {userData.Role}, Name: {userData.UserName}.");
                return serviceResult;
            }

            existingProperty.Place = request.PropertyRequest.Place;
            existingProperty.Price = request.PropertyRequest.Price;
            existingProperty.Description = request.PropertyRequest.Description;
            existingProperty.NearbyCollege = request.PropertyRequest.NearbyCollege;
            existingProperty.NearbyHospital = request.PropertyRequest.NearbyHospital;
            existingProperty.NearbySchool = request.PropertyRequest.NearbySchool;
            existingProperty.NoOfBathroom = request.PropertyRequest.NoOfBathroom;
            existingProperty.NoOfBedroom = request.PropertyRequest.NoOfBedroom;
            existingProperty.SizeinSqft = request.PropertyRequest.SizeinSqft;
            existingProperty.LastUpdated = DateTime.UtcNow;

            var updatedProperty = await propertyRepository.Update(existingProperty, cancellationToken);

            if (updatedProperty == false)
            {
                logger.LogError($"Failed to update property! PropertyId: {existingProperty.Id}, UserID: {userData.UserId}, UserRole: {userData.Role}, Name: {userData.UserName}.");
                serviceResult.StatusCode = HttpStatusCode.BadRequest;
                serviceResult.Message = "Failed to update property.";
                return serviceResult;
            }

            logger.LogInformation($"Successfully updated property! PropertyId: {existingProperty.Id}, UserID: {userData.UserId}, UserRole: {userData.Role}, Name: {userData.UserName}.");

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
