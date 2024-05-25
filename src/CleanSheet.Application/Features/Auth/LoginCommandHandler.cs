using CleanSheet.Domain.Errors;
using CleanSheet.Domain.Repositories;
using CleanSheet.Domain.Services;
using CleanSheet.Domain.Shared;
using MediatR;
using Hash = BCrypt.Net.BCrypt;

namespace CleanSheet.Application.Features.Auth;
public class LoginCommandHandler(
    IUserRepository userRepository,
    IJwtService jwtService) : IRequestHandler<LoginCommand, Result<LoginResponse>>
{
    public async Task<Result<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByEmailAsync(request.Email);

        if (user is null)
            return Result<LoginResponse>.Failure(AuthErrors.InvalidCredentials);

        var isPasswordValid = Hash.Verify(request.Password, user.PasswordHash);

        if (!isPasswordValid)
            return Result<LoginResponse>.Failure(AuthErrors.InvalidCredentials);

        var token = jwtService.Generate(user);

        var result = new LoginResponse(token);

        return Result<LoginResponse>.Success(result);
    }
}
