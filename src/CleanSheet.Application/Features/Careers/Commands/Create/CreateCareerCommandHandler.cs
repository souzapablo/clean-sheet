using CleanSheet.Domain.Entities;
using CleanSheet.Domain.Errors;
using CleanSheet.Domain.Repositories;
using CleanSheet.Domain.Shared;
using MediatR;

namespace CleanSheet.Application.Features.Careers.Commands.Create;

public class CreateCareerCommandHandler(
    ICareerRepository careerRepository,
    IUserRepository userRepository) : IRequestHandler<CreateCareerCommand, Result<long>>
{
    public async Task<Result<long>> Handle(CreateCareerCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.UserId, cancellationToken);

        if (user is null)
            return Result<long>.Failure(UserErrors.UserNotFound(request.UserId));

        var newCareer = new Career(request.Manager);

        user.AddCareer(newCareer);

        await careerRepository.AddAsync(newCareer, cancellationToken);
        
        return Result<long>.Success(newCareer.Id);
    }
}