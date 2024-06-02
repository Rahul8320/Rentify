using System.Net;
using MediatR;
using Rentify.Application.Models.Responses;
using Rentify.Application.Services.Interfaces;
using Rentify.Domain.Services;

namespace Rentify.Application.Queries.Handlers;

public class GetSellerPropertiesHandler(
    ILoggerService logger,
   IMediator mediator,
   IAuthService authService) : IRequestHandler<GetSellerProperties, RequestResponse<IEnumerable<PropertyResponse>>>
{
    public async Task<RequestResponse<IEnumerable<PropertyResponse>>> Handle(GetSellerProperties request, CancellationToken cancellationToken)
    {
        try
        {
            var serviceResult = new RequestResponse<IEnumerable<PropertyResponse>>();
            var userData = authService.GetAuthenticatedUserData();

            var query = new GetAllProperties();
            var propertiesResult = await mediator.Send(query, cancellationToken);

            if (propertiesResult.StatusCode != HttpStatusCode.OK)
            {
                return propertiesResult;
            }

            serviceResult.Data = propertiesResult.Data.Where(p => p.Owner == userData.UserId).ToList();
            return serviceResult;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            throw;
        }
    }
}
