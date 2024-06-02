using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mime;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rentify.Application.Commands;
using Rentify.Application.Models.Requests;
using Rentify.Application.Services.Interfaces;
using Rentify.Domain.Services;

namespace Rentity.Api.Controllers;

public class AuthController(
    ILoggerService logger,
    IAuthService authService,
    IMediator mediator) : BaseController
{
    [HttpPost]
    [Route("register")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            // Check for valid register user model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var command = new RegisterCommand(request);
            var response = await mediator.Send(command, cancellationToken);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return Problem(detail: response.Message, statusCode: (int)response.StatusCode);
            }

            return Ok();

        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            throw;
        }
    }

    [HttpPost]
    [Route("login")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        try
        {
            // Check for validation errors
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var command = new LoginCommand(request);
            // get jwt token
            var response = await mediator.Send(command);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return Problem(detail: response.Message, statusCode: (int)response.StatusCode);
            }

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(response.Data),
                expiration = response.Data.ValidTo
            });
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            throw;
        }
    }

    [HttpGet]
    [Route("verify")]
    [Authorize]
    [Produces(MediaTypeNames.Application.Json)]
    public IActionResult Verify()
    {
        try
        {
            var data = authService.GetAuthenticatedUserData();

            return Ok(data);
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message, ex);
            throw;
        }
    }
}
