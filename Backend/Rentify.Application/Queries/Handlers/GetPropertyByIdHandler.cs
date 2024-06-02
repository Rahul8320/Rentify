using System.Net;
using MediatR;
using Rentify.Application.Mapping;
using Rentify.Application.Models.Responses;
using Rentify.Application.Services;
using Rentify.Application.Services.Interfaces;
using Rentify.Domain.Repositories;
using Rentify.Domain.Services;

namespace Rentify.Application.Queries.Handlers;

public class GetPropertyByIdHandler(
    ILoggerService logger,
    ICachingService cachingService,
    IPropertyRepository propertyRepository,
    IAuthRepository authRepository) : IRequestHandler<GetPropertyById, RequestResponse<PropertyResponse>>
{
    public async Task<RequestResponse<PropertyResponse>> Handle(GetPropertyById request, CancellationToken cancellationToken)
    {
        try
        {
            var serviceResult = new RequestResponse<PropertyResponse>();

            logger.LogInformation(message: "Fetching data from cached...");
            var cachedProperty = cachingService.GetData<PropertyResponse>(CachingKeys.PropertyDetailsKey + request.PropertyId);

            if (cachedProperty != null)
            {
                logger.LogInformation(message: "Successfully fetch data from cached.");
                serviceResult.Data = cachedProperty;
                return serviceResult;
            }

            logger.LogWarning(message: "No data found in cached");

            logger.LogInformation(message: "Fetching data from database...");

            var propertyDetails = await propertyRepository.GetById(request.PropertyId, cancellationToken);

            if (propertyDetails == null)
            {
                logger.LogInformation(message: "Property not found!.");
                serviceResult.StatusCode = HttpStatusCode.NotFound;
                serviceResult.Message = $"Property with id: {request.PropertyId} not found in database!";
                return serviceResult;
            }

            logger.LogInformation(message: "Successfully fetch data from database..");

            var owner = await authRepository.GetUserById(propertyDetails.Owner);
            var mappedOwner = new UserResponse();

            if (owner != null)
            {
                mappedOwner = owner.ToModel();
            }

            // Mapped property to response model
            var mappedProperty = propertyDetails.ToModel(mappedOwner);

            // set data into cached
            cachingService.SetData<PropertyResponse>(CachingKeys.PropertyDetailsKey + mappedProperty.Id, mappedProperty);

            serviceResult.Data = mappedProperty;
            return serviceResult;
        }
        catch (Exception ex)
        {
            logger.LogError(message: ex.Message, exception: ex);
            throw;
        }
    }
}
