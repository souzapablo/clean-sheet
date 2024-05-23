using CleanSheet.Domain.Enums;
using CleanSheet.Domain.Shared;
using MediatR;

namespace CleanSheet.Application.Features.InitialTeams.Commands.AddInitialTeamPlayer;

public record AddInitialTeamPlayerCommand(
    string Slug,
    string Name,
    int KitNumber,
    int Overall,
    DateOnly Birthday,
    PlayerPosition Position) : IRequest<Result>;