using CleanSheet.Domain.Entities;
using CleanSheet.Domain.Errors;
using CleanSheet.Domain.Repositories;
using CleanSheet.Domain.Shared;
using MediatR;

namespace CleanSheet.Application.Features.InitialTeams.Commands.AddInitialTeamPlayer;

public class AddInitialTeamPlayerCommandHandler(
    IInitialTeamRepository initialTeamRepository) : IRequestHandler<AddInitialTeamPlayerCommand, Result>
{
    public async Task<Result> Handle(AddInitialTeamPlayerCommand request, CancellationToken cancellationToken)
    {
        var initialTeam = await initialTeamRepository.GetInitialTeamBySlugAsync(request.Slug, cancellationToken);

        if (initialTeam is null)
            return Result.Failure(InitialTeamErrors.InitialTeamNotFound(request.Slug));

        var player = new Player(request.Name, request.KitNumber, request.Overall, request.Birthday, request.Position);

        initialTeam.AddPlayer(player);

        await initialTeamRepository.UpdateAsync(initialTeam, cancellationToken);

        return Result.Success;
    }
}