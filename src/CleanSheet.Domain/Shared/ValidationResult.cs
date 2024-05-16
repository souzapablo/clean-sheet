namespace CleanSheet.Domain.Shared;

public sealed class ValidationResult : Result, IValidationResult
{
    internal ValidationResult(Error[] errors)
        : base(IValidationResult.ValidationError) =>
        Errors = errors;

    public Error[] Errors { get; }

    public static ValidationResult WithErrors(Error[] errors) =>
        new(errors);
}