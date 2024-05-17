using CleanSheet.Domain.Shared;
using MediatR;

namespace CleanSheet.Application.Features.InitialTeams.Commands.Create;

public record CreateInitialTeamCommand(
    string Name,
    string Stadium) : IRequest<TypedResult<Guid>>;