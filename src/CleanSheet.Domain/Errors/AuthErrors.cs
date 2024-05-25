using CleanSheet.Domain.Shared;

namespace CleanSheet.Domain.Errors;
public class AuthErrors
{
    public static Error InvalidCredentials => new Error(400, "InvalidCredentials", "Invalid credentials. Review the data and try again.");
}
