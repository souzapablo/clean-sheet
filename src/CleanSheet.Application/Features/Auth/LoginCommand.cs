using CleanSheet.Domain.Shared;
using MediatR;

namespace CleanSheet.Application.Features.Auth;
public record LoginCommand(
    string Email,
    string Password) : IRequest<Result<LoginResponse>>;
