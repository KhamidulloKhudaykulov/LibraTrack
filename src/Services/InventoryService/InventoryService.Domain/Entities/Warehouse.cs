using InventoryService.Domain.Primitives;
using InventoryService.Domain.Shared;

namespace InventoryService.Domain.Entities;

public class Warehouse : Entity
{
    private Warehouse(Guid productId, int availableQuantity)
    {
        ProductId = productId;
        AvailableQuantity = availableQuantity;
    }

    public Guid ProductId { get; set; }
    public int AvailableQuantity { get; set; }

    public Result<Warehouse> Create(Guid productId, int availableQuantity)
    {
        return new Warehouse(productId, availableQuantity);
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
