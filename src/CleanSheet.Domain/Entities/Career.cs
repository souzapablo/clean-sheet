using CleanSheet.Domain.Primitives;

namespace CleanSheet.Domain.Entities;

public class Career(string manager) : Entity
{
    private readonly List<Team> _teams = [];

    public string Manager { get; private set; } = manager;
    public DateTime LastUpdate { get; private set; } = DateTime.Now;
    public User User { get; private set; } = null!;
    public long UserId { get; private set; }
    public IReadOnlyCollection<Team> Teams => _teams;
    public Team CurrentTeam => _teams.OrderByDescending(t => t.Id)
        .First();

    public void SetInitialTeam(string teamName, string teamStadium, IReadOnlyCollection<Player> squad)
    {
        var newTeam = new Team(teamName, teamStadium);

        newTeam.AddSquad([.. squad]);

        _teams.Add(newTeam);
    }
}