namespace InventoryService.Domain.Primitives;

public class DomainEvent : IDomainEvent
{
    public DateTime OccuredOn => DateTime.UtcNow;
}
