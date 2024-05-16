using CleanSheet.Domain.Entities;

namespace CleanSheet.Domain.Repositories;

public interface ICareerRepository
{
    Task AddAsync(Career career, CancellationToken cancellationToken = default);
    Task<List<Career>> ListAsync(CancellationToken cancellationToken = default);
    Task<Career?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}