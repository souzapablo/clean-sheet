using CleanSheet.Domain.Errors;
using CleanSheet.Domain.Repositories;
using CleanSheet.Domain.Shared;
using MediatR;

namespace CleanSheet.Application.Features.InitialTeams.Commands.RemoveInitialTeamPlayer;

public class RemoveInitialTeamPlayerCommandHandler
    (IInitialTeamRepository initialTeamRepository) : IRequestHandler<RemoveInitialTeamPlayerCommand, Result>
{
    public async Task<Result> Handle(RemoveInitialTeamPlayerCommand request, CancellationToken cancellationToken)
    {
        var initialTeam = await initialTeamRepository.GetBySlugAsync(request.Slug, cancellationToken);

        if (initialTeam is null)
            return Result.Failure(InitialTeamErrors.InitialTeamNotFound(request.Slug));
        
        var removePlayer = initialTeam.RemovePlayer(request.PlayerName);

        if (!removePlayer.IsSuccess)
            return removePlayer;    

        await initialTeamRepository.UpdateAsync(initialTeam, cancellationToken);

        return removePlayer;
    }
}