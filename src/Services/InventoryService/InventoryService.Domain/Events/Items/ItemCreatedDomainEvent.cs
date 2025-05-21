using InventoryService.Domain.Primitives;
using MediatR;

namespace InventoryService.Domain.Events.Items;

public class ItemCreatedDomainEvent : DomainEvent, INotification
{
    public Guid ProductId { get; set; }

    public ItemCreatedDomainEvent(Guid productId)
    {
        ProductId = productId;
    }
}
