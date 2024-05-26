using CleanSheet.Domain.Shared;

namespace CleanSheet.Domain.Errors;
public class UserErrors
{
    public static Error UserNotFound(long id) => new(404, "UserNotFound",
        $"User with id {id} not found.");
}
