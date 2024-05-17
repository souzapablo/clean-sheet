using FluentValidation;

namespace CleanSheet.Application.Features.InitialTeams.Commands.Create;

public class CreateInitialTeamCommandValidator : AbstractValidator<CreateInitialTeamCommand>
{
    public CreateInitialTeamCommandValidator()
    {
        RuleFor(command => command.Name)
            .MinimumLength(5)
            .WithMessage("Initial team name must have at least 5 characters.");
        
        RuleFor(command => command.Stadium)
            .MinimumLength(5)
            .WithMessage("Initial team stadium must have at least 5 characters.");
    }
}