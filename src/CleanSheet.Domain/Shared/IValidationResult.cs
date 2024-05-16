namespace CleanSheet.Domain.Shared;

public interface IValidationResult
{
    public static readonly Error ValidationError =
        new("ValidationError", "One or more validation errors occured");

    Error[] Errors { get; }
}