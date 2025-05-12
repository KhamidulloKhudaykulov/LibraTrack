namespace BookService.Domain.Primitives;

public abstract class Entity
{
    public Guid Id { get; protected set; }
    protected Entity()
        => Id = Guid.NewGuid();

    protected Entity(Guid id)
        => Id = id;

    private List<IDomainEvent> _domainEvents = new();
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
    protected void AddDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);
    public void ClearDomainEvents() => _domainEvents.Clear();
    public IReadOnlyCollection<IDomainEvent> GetDomainEvents() => DomainEvents;
}
