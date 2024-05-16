namespace CleanSheet.Domain.Primitives;

public record Error(
    string Code,
    string Message);