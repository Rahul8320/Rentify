using Microsoft.EntityFrameworkCore;
using Rentify.Domain.Entities;
using Rentify.Domain.Repositories;
using Rentify.Domain.Services;
using Rentity.Infrastructure.Data;

namespace Rentity.Infrastructure.Repositories;

public class PropertyRepository(AppDbContext dbContext, ILoggerService logger) : IPropertyRepository
{
    public async Task<bool> Add(Property entity, CancellationToken cancellationToken)
    {
        try
        {
            await dbContext.Properties.AddAsync(entity, cancellationToken);
            return await Complete(cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(message: ex.Message, exception: ex);
            throw;
        }
    }

    public async Task<IEnumerable<Property>> GetAll(CancellationToken cancellationToken)
    {
        try
        {
            return await dbContext.Properties.Where(p => p.Status == 0).ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(message: ex.Message, exception: ex);
            throw;
        }
    }

    public async Task<Property?> GetById(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            return await dbContext.Properties.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id && p.Status == 0, cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(message: ex.Message, exception: ex);
            throw;
        }
    }

    public async Task<bool> Update(Property entity, CancellationToken cancellationToken)
    {
        try
        {
            dbContext.Properties.Update(entity);
            return await Complete(cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(message: ex.Message, exception: ex);
            throw;
        }
    }

    private async Task<bool> Complete(CancellationToken cancellationToken)
    {
        try
        {
            var result = await dbContext.SaveChangesAsync(cancellationToken);
            return result > 0;
        }
        catch (Exception ex)
        {
            logger.LogError(message: ex.Message, exception: ex);
            throw;
        }
    }
}
