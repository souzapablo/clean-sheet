using CleanSheet.Domain.Errors;
using CleanSheet.Domain.Repositories;
using CleanSheet.Domain.Shared;
using MediatR;

namespace CleanSheet.Application.Features.Careers.Queries.GetByUser;
public class GetCareerByUserIdQueryHandler
    (IUserRepository userRepository): IRequestHandler<GetCareerByUserIdQuery, Result<IEnumerable<CareerResponse>>>
{
    public async Task<Result<IEnumerable<CareerResponse>>> Handle(GetCareerByUserIdQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.Id, cancellationToken);

        if (user is null)
            return Result<IEnumerable<CareerResponse>>.Failure(UserErrors.UserNotFound(request.Id));

        var response = user.Careers
            .OrderByDescending(career => career.Id)
            .Select(carer => new CareerResponse(
                carer.Id,
                carer.Manager,
                carer.CurrentTeam.Name,
                carer.LastUpdate));

        return Result<IEnumerable<CareerResponse>>.Success(response);
    }
}
