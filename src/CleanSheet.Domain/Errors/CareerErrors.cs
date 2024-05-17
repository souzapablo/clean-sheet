using CleanSheet.Domain.Shared;

namespace CleanSheet.Domain.Errors;

public static class CareerErrors
{
    public static Error CareerNotFound(long id) => new(404,"CareerNotFound", $"Career with id {id} not found.");
}