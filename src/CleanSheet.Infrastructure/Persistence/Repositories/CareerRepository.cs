using CleanSheet.Domain.Entities;
using CleanSheet.Domain.Repositories;

namespace CleanSheet.Infrastructure.Persistence.Repositories;

public class CareerRepository(AppDbContext context) : ICareerRepository
{
    public async Task AddAsync(Career career, CancellationToken cancellationToken)
    {
        context.Add(career);
        await context.SaveChangesAsync(cancellationToken);
    }
}