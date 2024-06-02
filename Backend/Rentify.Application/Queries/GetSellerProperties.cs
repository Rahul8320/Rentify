using MediatR;
using Rentify.Application.Models.Responses;

namespace Rentify.Application.Queries;

public record GetSellerProperties : IRequest<RequestResponse<IEnumerable<PropertyResponse>>>;