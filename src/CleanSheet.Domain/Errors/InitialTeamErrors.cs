using CleanSheet.Domain.Shared;

namespace CleanSheet.Domain.Errors;

public static class InitialTeamErrors
{
    public static Error InitialTeamAlreadyExists(string slug) => new(400, "InitialTeamAlreadyExists",
        $"There is already a team registered with slug {slug}.");
}