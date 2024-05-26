using CleanSheet.Domain.Entities;
using CleanSheet.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CleanSheet.Infrastructure.Persistence.Repositories;

public class CareerRepository(AppDbContext context) : ICareerRepository
{
    public async Task AddAsync(Career career, CancellationToken cancellationToken = default)
    {
        context.Add(career);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<Career>> ListAsync(CancellationToken cancellationToken = default) =>
        await context.Careers
            .Include(career => career.Teams)
            .ToListAsync(cancellationToken);

    public async Task<Career?> GetByIdAsync(long id, CancellationToken cancellationToken = default) =>
        await context.Careers
            .Include(career => career.Teams)
            .ThenInclude(team => team.Squad)
            .SingleOrDefaultAsync(
                career => career.Id == id, 
                cancellationToken);

    public async Task UpdateAsync(Career career, CancellationToken cancellationToken = default)
    {
        context.Careers.Update(career);
        await context.SaveChangesAsync(cancellationToken);
    }
}