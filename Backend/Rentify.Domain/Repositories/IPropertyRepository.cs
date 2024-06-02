using Rentify.Domain.Entities;

namespace Rentify.Domain.Repositories;

public interface IPropertyRepository
{
    Task<IEnumerable<Property>> GetAll(CancellationToken cancellationToken);
    Task<Property?> GetById(Guid id, CancellationToken cancellationToken);
    Task<bool> Add(Property entity, CancellationToken cancellationToken);
    Task<bool> Update(Property entity, CancellationToken cancellationToken);
}
