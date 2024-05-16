using FluentValidation;

namespace CleanSheet.Application.Features.Careers.Commands.Create;

public class CreateCareerCommandValidator : AbstractValidator<CreateCareerCommand>
{
    public CreateCareerCommandValidator()
    {
        RuleFor(command => command.Manager)
            .Length(3)
            .WithMessage("Manager name must have at least 3 characters.");
    }
}