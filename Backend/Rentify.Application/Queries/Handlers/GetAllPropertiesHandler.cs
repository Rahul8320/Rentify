using System.Net;
using MediatR;
using Rentify.Application.Mapping;
using Rentify.Application.Models.Responses;
using Rentify.Application.Services;
using Rentify.Application.Services.Interfaces;
using Rentify.Domain.Repositories;
using Rentify.Domain.Services;

namespace Rentify.Application.Queries.Handlers;

public class GetAllPropertiesHandler(
    ILoggerService logger,
    ICachingService cachingService,
    IPropertyRepository propertyRepository) : IRequestHandler<GetAllProperties, RequestResponse<IEnumerable<PropertyResponse>>>
{
    public async Task<RequestResponse<IEnumerable<PropertyResponse>>> Handle(GetAllProperties request, CancellationToken cancellationToken)
    {
        try
        {
            var serviceResult = new RequestResponse<IEnumerable<PropertyResponse>>();

            logger.LogInformation(message: "Fetching data from cached...");
            var cachedProperties = cachingService.GetData<IEnumerable<PropertyResponse>>(CachingKeys.AllPropertyKey);

            if (cachedProperties != null && cachedProperties.Any())
            {
                logger.LogInformation(message: "Successfully fetch data from cached.");
                serviceResult.Data = cachedProperties;
                return serviceResult;
            }

            logger.LogWarning(message: "No data found in cached.");

            logger.LogInformation(message: "Fetching data from database...");
            var properties = await propertyRepository.GetAll(cancellationToken);

            if (properties == null || properties.Any() == false)
            {
                logger.LogWarning(message: "No data found in database.");
                serviceResult.StatusCode = HttpStatusCode.NotFound;
                serviceResult.Message = "No data found in database!";
                return serviceResult;
            }

            logger.LogInformation(message: "Successfully fetch data from database.");

            // Mapped property to response model
            var mappedPropertites = properties.OrderByDescending(p => p.LastUpdated).Select(p => p.ToModel());

            // set data into cached
            cachingService.SetData<IEnumerable<PropertyResponse>>(CachingKeys.AllPropertyKey, mappedPropertites);

            // sort by last update
            serviceResult.Data = mappedPropertites;
            return serviceResult;
        }
        catch (Exception ex)
        {
            logger.LogError(message: ex.Message, exception: ex);
            throw;
        }
    }
}
