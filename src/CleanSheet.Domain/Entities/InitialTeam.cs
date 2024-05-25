using CleanSheet.Domain.Errors;
using CleanSheet.Domain.Primitives;
using CleanSheet.Domain.Shared;

namespace CleanSheet.Domain.Entities;

public class InitialTeam(
    string name,
    string stadium,
    string slug) : Entity
{
    private readonly List<Player> _players = [];
    public string Name { get; private set; } = name;
    public string Stadium { get; private set; } = stadium;
    public string Slug { get; private set; } = slug;

    public IReadOnlyCollection<Player> Players => _players;

    public void AddPlayer(Player player) =>
        _players.Add(player);

    public Result RemovePlayer(string playerName)
    {
        var playerToRemove = _players.SingleOrDefault(player => player.Name.Equals(playerName));

        if (playerToRemove is null)
            return Result.Failure(InitialTeamErrors.PlayerNotFound(Name, playerName));

        _players.Remove(playerToRemove);

        return Result.Success;
    }
}