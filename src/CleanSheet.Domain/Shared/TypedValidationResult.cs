namespace CleanSheet.Domain.Shared;

public class TypedValidationResult<TValue> :  Result, IValidationResult
{
    private TypedValidationResult(Error[] errors)
        : base(IValidationResult.ValidationError) =>
        Errors = errors;

    public Error[] Errors { get; }

    public static TypedValidationResult<TValue> WithErrors(Error[] errors) =>
        new(errors);
}