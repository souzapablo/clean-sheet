using CleanSheet.Domain.Primitives;

namespace CleanSheet.Domain.Entities;

public class Career(string manager) : Entity
{
    public string Manager { get; private set; } = manager;
    public DateTime LastUpdate { get; private set; } = DateTime.Now;
    public User User { get; private set; } = null!;
    public long UserId { get; private set; }
}