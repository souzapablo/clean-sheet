using CleanSheet.Domain.Primitives;

namespace CleanSheet.Domain.Entities;
public class Team(string name, string stadium) : Entity
{
    private readonly List<Player> _squad = [];

    public string Name { get; private set; } = name;
    public string Stadium { get; private set; } = stadium;
    public Career Career { get; private set; } = null!;
    public long CareerId { get; private set; }
    public IReadOnlyCollection<Player> Squad => _squad;

    public void AddSquad(List<Player> squad) =>
        _squad.AddRange(squad);
}
