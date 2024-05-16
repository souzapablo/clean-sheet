namespace CleanSheet.Domain.Primitives;

public abstract class Entity(Guid id)
{
    public Guid Id { get; private set; } = id;
    public bool IsDeleted { get; private set; }

    protected void Delete() =>
        IsDeleted = true;
}