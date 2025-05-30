using InventoryService.Domain.Events.Items;
using InventoryService.Domain.Primitives;
using InventoryService.Domain.Shared;

namespace InventoryService.Domain.Entities;

public class StockEntry : Entity
{
    protected StockEntry(
        Guid productId,
        int amount,
        decimal price)
    {
        ProductId = productId;
        Amount = amount;
        Price = price;
    }

    public Guid ProductId { get; private set; }
    public int Amount { get; private set; }
    public decimal Price { get; private set; }
    public decimal TotalPrice  => Amount * Price;
    public DateTime CreatedAt { get; } = DateTime.UtcNow;

    public static Result<StockEntry> Create(
        Guid productId,
        int amount,
        int availableQuantity,
        decimal price
        )
    {
        
        var item = new StockEntry(productId, amount, price);
        item.AddDomainEvent(new ItemCreatedDomainEvent(productId));

        return item;
    }
}
