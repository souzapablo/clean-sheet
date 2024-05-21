using CleanSheet.Domain.Shared;

namespace CleanSheet.Domain.Errors;

public static class InitialTeamErrors
{
    public static Error InitialTeamAlreadyExists(string slug) => new(400, "InitialTeamAlreadyExists",
        $"There is already an initial team registered with slug {slug}.");
    public static Error InitialTeamNotFound(string slug) => new(404, "InitialTeamNotFound", 
        $"Initial team with slug {slug} not found.");
}