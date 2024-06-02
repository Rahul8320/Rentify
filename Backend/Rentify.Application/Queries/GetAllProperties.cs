using MediatR;
using Rentify.Application.Models.Responses;

namespace Rentify.Application.Queries;

public record GetAllProperties : IRequest<RequestResponse<IEnumerable<PropertyResponse>>>;
