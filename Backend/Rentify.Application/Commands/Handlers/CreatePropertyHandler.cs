using System.Net;
using MediatR;
using Rentify.Application.Mapping;
using Rentify.Application.Models.Responses;
using Rentify.Application.Services;
using Rentify.Application.Services.Interfaces;
using Rentify.Domain.Entities;
using Rentify.Domain.Enums;
using Rentify.Domain.Repositories;
using Rentify.Domain.Services;

namespace Rentify.Application.Commands.Handlers;

public class CreatePropertyHandler(
    IPropertyRepository propertyRepository,
    IAuthService authService,
    ICachingService cachingService,
    ILoggerService logger) : IRequestHandler<CreateProperty, RequestResponse<PropertyResponse>>
{
    public async Task<RequestResponse<PropertyResponse>> Handle(CreateProperty request, CancellationToken cancellationToken)
    {
        try
        {
            var serviceResult = new RequestResponse<PropertyResponse>();

            // Get logged in user data
            var userData = authService.GetAuthenticatedUserData();

            if (UserRole.Seller.ToString().Equals(userData.Role, StringComparison.OrdinalIgnoreCase) == false)
            {
                serviceResult.StatusCode = HttpStatusCode.Unauthorized;
                serviceResult.Message = "Not authorized to do this operation!";
                logger.LogWarning($"Unauthorized create request! UserID: {userData.UserId}, UserRole: {userData.Role}, Name: {userData.UserName}.");
                return serviceResult;
            }

            logger.LogInformation(message: "Creating new propertry...");

            var property = new Property
            {
                Id = Guid.NewGuid(),
                Owner = userData.UserId,
                Place = request.PropertyRequest.Place,
                Price = request.PropertyRequest.Price,
                Description = request.PropertyRequest.Description,
                NearbyCollege = request.PropertyRequest.NearbyCollege,
                NearbyHospital = request.PropertyRequest.NearbyHospital,
                NearbySchool = request.PropertyRequest.NearbySchool,
                NoOfBathroom = request.PropertyRequest.NoOfBathroom,
                NoOfBedroom = request.PropertyRequest.NoOfBedroom,
                SizeinSqft = request.PropertyRequest.SizeinSqft,
            };

            var createdProperty = await propertyRepository.Add(property, cancellationToken);

            if (createdProperty == false)
            {
                logger.LogError($"Failed to create new property! UserID: {userData.UserId}, UserRole: {userData.Role}, Name: {userData.UserName}.");
                serviceResult.StatusCode = HttpStatusCode.BadRequest;
                serviceResult.Message = "Failed to create new property.";
                return serviceResult;
            }

            logger.LogInformation($"Successfully created new property! UserID: {userData.UserId}, UserRole: {userData.Role}, Name: {userData.UserName}.");

            // Removed cached for all property
            cachingService.RemoveData(CachingKeys.AllPropertyKey);

            serviceResult.Data = property.ToModel();
            return serviceResult;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            throw;
        }
    }
}
