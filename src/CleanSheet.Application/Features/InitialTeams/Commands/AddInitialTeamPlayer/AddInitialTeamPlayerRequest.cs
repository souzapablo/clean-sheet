using CleanSheet.Domain.Enums;

namespace CleanSheet.Application.Features.InitialTeams.Commands.AddInitialTeamPlayer;

public record AddInitialTeamPlayerRequest(
    string Name,
    int KitNumber,
    int Overall,
    DateOnly Birthday,
    PlayerPosition Position);