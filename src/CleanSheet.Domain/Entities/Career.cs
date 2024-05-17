using CleanSheet.Domain.Primitives;

namespace CleanSheet.Domain.Entities;

public class Career(string manager) : Entity
{
    public string Manager { get; private set; } = manager;
    public DateTime LastUpdate { get; private set; } = DateTime.Now;
}