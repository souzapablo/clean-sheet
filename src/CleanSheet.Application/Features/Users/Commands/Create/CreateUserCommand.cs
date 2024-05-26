using CleanSheet.Domain.Enums;
using CleanSheet.Domain.Shared;
using MediatR;

namespace CleanSheet.Application.Features.Users.Commands.Create;
public record CreateUserCommand(
    string Name,
    string Email,
    string Password,
    UserRole Role) : IRequest<Result<long>>;