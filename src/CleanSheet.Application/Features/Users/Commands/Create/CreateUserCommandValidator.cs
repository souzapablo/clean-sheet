using FluentValidation;

namespace CleanSheet.Application.Features.Users.Commands.Create;
public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(command => command.Name)
            .MinimumLength(5)
            .WithMessage("User name must have at least 5 characters.");

        RuleFor(command => command.Email)
            .EmailAddress()
            .WithMessage("Invalid e-mail address.");

        RuleFor(command => command.Role)
            .IsInEnum()
            .WithMessage("Invalid role.");

        RuleFor(command => command.Password)
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$")
            .WithMessage(
                "Password must contain at least eight characters, one uppercase letter, one lowercase letter, one number and one special character");
    }
}
