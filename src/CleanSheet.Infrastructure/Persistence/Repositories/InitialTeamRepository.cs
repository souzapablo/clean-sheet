using CleanSheet.Domain.Entities;
using CleanSheet.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CleanSheet.Infrastructure.Persistence.Repositories;

public class InitialTeamRepository(AppDbContext context) : IInitialTeamRepository
{
    public async Task AddAsync(InitialTeam initialTeam, CancellationToken cancellationToken = default)
    {
        context.InitialTeams.Add(initialTeam);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<InitialTeam>> ListAsync(CancellationToken cancellationToken = default) =>
        await context.InitialTeams
            .ToListAsync(cancellationToken);

    public async Task<InitialTeam?> GetInitialTeamBySlugAsync(string slug,
        CancellationToken cancellationToken = default)
        => await context.InitialTeams
            .SingleOrDefaultAsync(initialTeam => initialTeam.Slug.Equals(slug), cancellationToken);

    public async Task UpdateAsync(InitialTeam initialTeam, CancellationToken cancellationToken = default)
    {
        context.InitialTeams.Update(initialTeam);
        await context.SaveChangesAsync(cancellationToken);
    }
}