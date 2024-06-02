using MediatR;
using Rentify.Application.Models.Requests;
using Rentify.Application.Models.Responses;

namespace Rentify.Application.Commands;

public record UpdateProperty(UpdatePropertyRequest PropertyRequest) : IRequest<RequestResponse>;
