using CleanSheet.Domain.Shared;
using FluentValidation;
using MediatR;
using ValidationResult = CleanSheet.Domain.Shared.ValidationResult;

namespace CleanSheet.Application.Behaviors;

public class ValidatorPipelineBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!validators.Any())
            return await next();

        var failures = validators
            .Select(validator => validator.Validate(request))
            .SelectMany(result => result.Errors)
            .Where(failure => failure is not null)
            .Select(failure => new Error(400, failure.PropertyName, failure.ErrorMessage))
            .Distinct()
            .ToArray();

        if (failures.Length != 0)
            return CreateValidationResult<TResponse>(failures);

        return await next();
    }

    private static TResult CreateValidationResult<TResult>(Error[] failures)
        where TResult : Result
    {
        if (typeof(TResult) == typeof(Result))
            return (ValidationResult.WithErrors(failures) as TResult)!;

        var validationResult = typeof(TypedValidationResult<>)
            .GetGenericTypeDefinition()
            .MakeGenericType(typeof(TResult).GenericTypeArguments[0])
            .GetMethod(nameof(ValidationResult.WithErrors))!
            .Invoke(null, [failures])!;

        return (TResult)validationResult;
    }

}