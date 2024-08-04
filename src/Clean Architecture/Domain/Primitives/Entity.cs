namespace Domain.Primitives;

public abstract class Entity<TEntityId> : IEntity
{
    private readonly List<DomainEvent> _domainEvents = new();

    protected Entity(TEntityId id)
    {
        Id = id;
    }

    protected Entity()
    {
    }

    public TEntityId Id { get; init; }

    public IReadOnlyCollection<DomainEvent> GetDomainEvents() => _domainEvents.ToList();

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    protected void Raise(DomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}
