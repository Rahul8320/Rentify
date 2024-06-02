using System.Net;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rentify.Application.Commands;
using Rentify.Application.Models.Requests;
using Rentify.Application.Queries;
using Rentify.Domain.Enums;
using Rentify.Domain.Services;

namespace Rentity.Api.Controllers;

public class PropertyController(
    ILoggerService logger,
    IMediator mediator) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        try
        {
            var query = new GetAllProperties();
            var response = await mediator.Send(query, cancellationToken);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return Problem(detail: response.Message, statusCode: (int)response.StatusCode);
            }

            return Ok(response.Data);
        }
        catch (Exception ex)
        {
            logger.LogError(message: ex.Message, exception: ex);
            throw;
        }
    }

    [HttpGet]
    [Route("/seller")]
    [Authorize(Roles = "Seller")]
    public async Task<IActionResult> GetSellerProperties(CancellationToken cancellationToken)
    {
        try
        {
            var query = new GetSellerProperties();
            var response = await mediator.Send(query, cancellationToken);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return Problem(detail: response.Message, statusCode: (int)response.StatusCode);
            }

            return Ok(response.Data);
        }
        catch (Exception ex)
        {
            logger.LogError(message: ex.Message, exception: ex);
            throw;
        }
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetDetail(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            var query = new GetPropertyById(id);
            var response = await mediator.Send(query, cancellationToken);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return Problem(detail: response.Message, statusCode: (int)response.StatusCode);
            }

            return Ok(response.Data);
        }
        catch (Exception ex)
        {
            logger.LogError(message: ex.Message, exception: ex);
            throw;
        }
    }

    [HttpGet]
    [Route("{id:guid}/liked")]
    [Authorize]
    public async Task<IActionResult> LikeProperty(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            var command = new LikeProperty(id);
            var response = await mediator.Send(command, cancellationToken);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return Problem(detail: response.Message, statusCode: (int)response.StatusCode);
            }

            return Ok(response.Data);
        }
        catch (Exception ex)
        {
            logger.LogError(message: ex.Message, exception: ex);
            throw;
        }
    }

    [HttpPost]
    [Authorize(Roles = "Seller")]
    public async Task<IActionResult> Add([FromBody] CreatePropertyRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            var command = new CreateProperty(request);
            var response = await mediator.Send(command, cancellationToken);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return Problem(detail: response.Message, statusCode: (int)response.StatusCode);
            }

            return CreatedAtAction(nameof(GetDetail), new { id = response.Data.Id }, response.Data);
        }
        catch (Exception ex)
        {
            logger.LogError(message: ex.Message, exception: ex);
            throw;
        }
    }

    [HttpPut]
    [Authorize(Roles = "Seller")]
    public async Task<IActionResult> UpdateProperty([FromBody] UpdatePropertyRequest request, CancellationToken cancellationToken = default)
    {
        try
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            var command = new UpdateProperty(request);
            var response = await mediator.Send(command, cancellationToken);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return Problem(detail: response.Message, statusCode: (int)response.StatusCode);
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(message: ex.Message, exception: ex);
            throw;
        }
    }

    [HttpDelete]
    [Route("{id:guid}")]
    [Authorize(Roles = "Seller")]
    public async Task<IActionResult> DeleteProperty(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var command = new DeleteProperty(id);
            var response = await mediator.Send(command, cancellationToken);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return Problem(detail: response.Message, statusCode: (int)response.StatusCode);
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(message: ex.Message, ex);
            throw;
        }
    }
}
