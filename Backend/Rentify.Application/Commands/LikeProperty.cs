using MediatR;
using Rentify.Application.Models.Responses;

namespace Rentify.Application.Commands;

public record LikeProperty(Guid PropertyId) : IRequest<RequestResponse<PropertyResponse>>;