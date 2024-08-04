namespace Domain.Primitives;

public interface IEntity
{
    IReadOnlyCollection<DomainEvent> GetDomainEvents();

    void ClearDomainEvents();
}