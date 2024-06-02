using MediatR;
using Rentify.Application.Models.Responses;

namespace Rentify.Application.Commands;

public record DeleteProperty(Guid PropertyId) : IRequest<RequestResponse>;

