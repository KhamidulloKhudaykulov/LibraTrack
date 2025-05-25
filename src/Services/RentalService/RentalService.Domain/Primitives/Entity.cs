namespace RentalService.Domain.Primitives;

public abstract class Entity
{
    public Guid Id { get; protected set; }
    public DateTime CreatedAt { get; protected set; }
    public bool IsDeleted { get; protected set; } = false;
    public Entity()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
    }
    public Entity(Guid id)
    {
        Id = id;
        CreatedAt = DateTime.UtcNow;
    }

    private List<IDomainEvent> _domainEvents = new();
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
    public void AddDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);
    public void ClearDomainEvents() => _domainEvents.Clear();
    public IReadOnlyCollection<IDomainEvent> GetDomainEvents() => DomainEvents;
}
