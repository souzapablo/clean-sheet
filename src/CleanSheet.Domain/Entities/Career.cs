using CleanSheet.Domain.Primitives;

namespace CleanSheet.Domain.Entities;

public class Career(Guid id, string manager) : Entity(id)
{
    public string Manager { get; private set; } = manager;
    public DateTime LastUpdate { get; private set; } = DateTime.Now;
}