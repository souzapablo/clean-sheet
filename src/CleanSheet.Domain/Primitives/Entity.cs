namespace CleanSheet.Domain.Primitives;

public abstract class Entity
{
    public long Id { get; private set; }
    public bool IsDeleted { get; private set; }

    public void Delete() =>
        IsDeleted = true;
}