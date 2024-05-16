using CleanSheet.Domain.Entities;
using CleanSheet.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CleanSheet.Infrastructure.Persistence.Repositories;

public class CareerRepository(AppDbContext context) : ICareerRepository
{
    public async Task AddAsync(Career career, CancellationToken cancellationToken)
    {
        context.Add(career);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<Career>> GetCareersAsync(CancellationToken cancellationToken) =>
        await context.Careers
            .Where(career => !career.IsDeleted)
            .ToListAsync(cancellationToken: cancellationToken);
}