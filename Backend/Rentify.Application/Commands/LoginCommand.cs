using System.IdentityModel.Tokens.Jwt;
using MediatR;
using Rentify.Application.Models.Requests;
using Rentify.Application.Models.Responses;

namespace Rentify.Application.Commands;

public record LoginCommand(LoginRequest LoginRequest) : IRequest<RequestResponse<JwtSecurityToken>>;