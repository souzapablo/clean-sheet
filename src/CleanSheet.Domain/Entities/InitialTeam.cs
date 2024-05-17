using CleanSheet.Domain.Primitives;

namespace CleanSheet.Domain.Entities;

public class InitialTeam(
    Guid id,
    string name,
    string stadium,
    string slug) : Entity(id)
{
    private List<Player> _players = [];
    public string Name { get; private set; } = name;
    public string Stadium { get; private set; } = stadium;
    public string Slug { get; private set; } = slug;
    public IReadOnlyCollection<Player> Players => _players;
}