using CleanSheet.Domain.Entities;

namespace CleanSheet.Domain.Repositories;

public interface ICareerRepository
{
    Task AddAsync(Career career, CancellationToken cancellationToken = default);
    Task<List<Career>> ListAsync(CancellationToken cancellationToken = default);
    Task<Career?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    Task UpdateAsync(Career career, CancellationToken cancellationToken = default);
}