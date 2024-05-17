using CleanSheet.Domain.Entities;
using CleanSheet.Domain.Errors;
using CleanSheet.Domain.Extensions;
using CleanSheet.Domain.Repositories;
using CleanSheet.Domain.Shared;
using MediatR;

namespace CleanSheet.Application.Features.InitialTeams.Commands.Create;

public class CreateInitialTeamCommandHandler(IInitialTeamRepository repository) : IRequestHandler<CreateInitialTeamCommand, TypedResult<long>>
{
    public async Task<TypedResult<long>> Handle(CreateInitialTeamCommand request, CancellationToken cancellationToken)
    {
        var slug = request.Name.CreateSlug();

        var initialTeam = await repository.GetInitialTeamBySlugAsync(slug, cancellationToken);
        
        if (initialTeam is not null)
            return TypedResult<long>.Failure(InitialTeamErrors.InitialTeamAlreadyExists(slug));
        
        var newInitialTeam = new InitialTeam(
            request.Name,
            request.Stadium, 
            slug);

        await repository.AddAsync(newInitialTeam, cancellationToken);

        return TypedResult<long>.Success(newInitialTeam.Id);
    }
}