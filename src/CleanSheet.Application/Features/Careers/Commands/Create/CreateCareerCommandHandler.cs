using CleanSheet.Domain.Entities;
using CleanSheet.Domain.Repositories;
using CleanSheet.Domain.Shared;
using MediatR;

namespace CleanSheet.Application.Features.Careers.Commands.Create;

public class CreateCareerCommandHandler(ICareerRepository careerRepository) : IRequestHandler<CreateCareerCommand, TypedResult<long>>
{
    public async Task<TypedResult<long>> Handle(CreateCareerCommand request, CancellationToken cancellationToken)
    {
        var newCareer = new Career(request.Manager);

        await careerRepository.AddAsync(newCareer, cancellationToken);
        
        return TypedResult<long>.Success(newCareer.Id);
    }
}