using CleanSheet.Domain.Errors;
using CleanSheet.Domain.Repositories;
using CleanSheet.Domain.Shared;
using MediatR;

namespace CleanSheet.Application.Features.Careers.Commands.Delete;

public class DeleteCareerCommandHandler(ICareerRepository careerRepository) : IRequestHandler<DeleteCareerCommand, Result>
{
    public async Task<Result> Handle(DeleteCareerCommand request, CancellationToken cancellationToken)
    {
        var career = await careerRepository.GetByIdAsync(request.Id, cancellationToken);
        
        if (career is null)
            return Result.Failure(CareerErrors.CareerNotFound(request.Id));
        
        career.Delete();

        await careerRepository.UpdateAsync(career, cancellationToken);

        return Result.Success;
    }
}