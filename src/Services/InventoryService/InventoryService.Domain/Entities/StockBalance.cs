using InventoryService.Domain.Primitives;
using InventoryService.Domain.Shared;

namespace InventoryService.Domain.Entities;

public class StockBalance : Entity
{
    private StockBalance(Guid productId, int availableQuantity)
    {
        ProductId = productId;
        AvailableQuantity = availableQuantity;
    }

    public Guid ProductId { get; set; }
    public int AvailableQuantity { get; set; }

    public static Result<StockBalance> Create(Guid productId, int availableQuantity)
    {
        return new StockBalance(productId, availableQuantity);
    }

    public void AddQuantity(int quantity)
    {
        AvailableQuantity += quantity;
    }

    public void RemoveQuantity(int quantity)
    {
        AvailableQuantity -= quantity;
    }
}
