using FluentValidation;

namespace CleanSheet.Application.Features.InitialTeams.Commands.AddInitialTeamPlayer;

public class AddInitialTeamPlayerValidator : AbstractValidator<AddInitialTeamPlayerCommand>
{
    public AddInitialTeamPlayerValidator()
    {
        RuleFor(command => command.Name)
            .MinimumLength(3)
            .WithMessage("Player name must have at least 3 characters.");

        RuleFor(command => command.Overall)
            .InclusiveBetween(1, 99)
            .WithMessage("Overall must be between 1 and 99");

        RuleFor(command => command.KitNumber)
            .InclusiveBetween(1, 99)
            .WithMessage("Kit number must be between 1 and 99");

        RuleFor(command => command.Position)
            .IsInEnum()
            .WithMessage("Invalid position value.");

        RuleFor(command => command.Birthday)
            .Must(IsAgeValid)
            .WithMessage("Player must have between 15 and 45");
    }

    private static bool IsAgeValid(DateOnly birthday)
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        var age = today.Year - birthday.Year;

        if (birthday > today.AddYears(-age))
            age--;

        return age is >= 15 and <= 45;
    }
}