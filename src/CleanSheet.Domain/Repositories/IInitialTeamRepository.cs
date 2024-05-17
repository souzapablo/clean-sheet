using CleanSheet.Domain.Entities;

namespace CleanSheet.Domain.Repositories;

public interface IInitialTeamRepository
{
    Task AddAsync(InitialTeam initialTeam, CancellationToken cancellationToken = default);
    Task<List<InitialTeam>> ListAsync(CancellationToken cancellationToken = default);
    Task<InitialTeam?> GetInitialTeamBySlugAsync(string slug, CancellationToken cancellationToken = default);
}