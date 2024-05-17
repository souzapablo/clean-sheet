namespace CleanSheet.Domain.Shared;

public record Error(
    int Status,
    string Code,
    string Message);