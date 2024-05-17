using CleanSheet.Domain.Entities;
using CleanSheet.Domain.Errors;
using CleanSheet.Domain.Extensions;
using CleanSheet.Domain.Repositories;
using CleanSheet.Domain.Shared;
using MediatR;

namespace CleanSheet.Application.Features.InitialTeams.Commands.Create;

public class CreateInitialTeamCommandHandler(IInitialTeamRepository repository) : IRequestHandler<CreateInitialTeamCommand, TypedResult<Guid>>
{
    public async Task<TypedResult<Guid>> Handle(CreateInitialTeamCommand request, CancellationToken cancellationToken)
    {
        var slug = request.Name.CreateSlug();

        var initialTeam = await repository.GetInitialTeamBySlugAsync(slug, cancellationToken);
        
        if (initialTeam is not null)
            return TypedResult<Guid>.Failure(InitialTeamErrors.InitialTeamAlreadyExists(slug));
        
        var newInitialTeam = new InitialTeam(
            Guid.NewGuid(),
            request.Name,
            request.Stadium, 
            slug);

        await repository.AddAsync(newInitialTeam, cancellationToken);

        return TypedResult<Guid>.Success(newInitialTeam.Id);
    }
}