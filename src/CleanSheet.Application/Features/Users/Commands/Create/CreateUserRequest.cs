using CleanSheet.Domain.Errors;

namespace CleanSheet.Application.Features.Users.Commands.Create;
public record CreateUserRequest(
    string Name,
    string Email,
    string Password,
    UserRole Role);