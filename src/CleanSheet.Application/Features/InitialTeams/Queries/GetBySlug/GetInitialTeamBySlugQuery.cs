using CleanSheet.Domain.Shared;
using MediatR;

namespace CleanSheet.Application.Features.InitialTeams.Queries.GetById;
public record GetInitialTeamBySlugQuery(string Slug) : IRequest<Result<InitialTeamResponse>>;