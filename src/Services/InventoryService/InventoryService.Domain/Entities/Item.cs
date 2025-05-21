using InventoryService.Domain.Primitives;
using InventoryService.Domain.Shared;

namespace InventoryService.Domain.Entities;

public class Item : Entity
{
    protected Item(
        Guid productId,
        int amount,
        int availableQuantity,
        decimal price)
    {
        ProductId = productId;
        Amount = amount;
        AvailableQuantity = availableQuantity;
        Price = price;
    }

    public Guid ProductId { get; private set; }
    public int Amount { get; private set; }
    public int AvailableQuantity { get; private set; }
    public decimal Price { get; private set; }
    public decimal TotalPrice => Amount * Price;
    public DateTime CreatedAt { get; } = DateTime.UtcNow;

    public static Result<Item> Create(
        Guid productId,
        int amount,
        int availableQuantity,
        decimal price
        )
    {
        return new Item(productId, amount, availableQuantity, price);
    }
}
