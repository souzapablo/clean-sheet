namespace CleanSheet.Domain.Shared;

public interface IValidationResult
{
    public static readonly Error ValidationError =
        new(400,"ValidationError", "One or more validation errors occured");

    Error[] Errors { get; }
}