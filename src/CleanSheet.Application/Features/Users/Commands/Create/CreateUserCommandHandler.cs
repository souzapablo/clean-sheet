using CleanSheet.Domain.Entities;
using CleanSheet.Domain.Repositories;
using CleanSheet.Domain.Shared;
using MediatR;
using Hash = BCrypt.Net.BCrypt;

namespace CleanSheet.Application.Features.Users.Commands.Create;
public class CreateUserCommandHandler(IUserRepository userRepository) : IRequestHandler<CreateUserCommand, Result<long>>
{
    public async Task<Result<long>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    { 
        var passwordHash = Hash.HashPassword(request.Password);

        var user = new User(request.Name, request.Email, passwordHash, request.Role);

        await userRepository.AddAsync(user);

        return Result<long>.Success(user.Id);
    }
}
