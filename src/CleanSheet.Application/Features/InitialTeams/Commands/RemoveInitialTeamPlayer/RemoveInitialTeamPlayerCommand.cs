using CleanSheet.Domain.Shared;
using MediatR;

namespace CleanSheet.Application.Features.InitialTeams.Commands.RemoveInitialTeamPlayer;

public record RemoveInitialTeamPlayerCommand(
    string Slug,
    string PlayerName) : IRequest<Result>;