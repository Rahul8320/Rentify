using MediatR;
using Rentify.Application.Models.Responses;

namespace Rentify.Application.Queries;

public record GetPropertyById(Guid PropertyId) : IRequest<RequestResponse<PropertyResponse>>;
