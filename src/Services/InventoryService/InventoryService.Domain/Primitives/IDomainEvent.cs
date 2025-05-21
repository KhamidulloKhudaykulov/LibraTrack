namespace InventoryService.Domain.Primitives;

public interface IDomainEvent
{
    DateTime OccuredOn { get; }
}
