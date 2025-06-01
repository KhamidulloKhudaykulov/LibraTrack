namespace InventoryService.Application.UseCases.Items.Contracts;

public class GetStockBalanceResponse
{
    public Guid Id { get; set; }
    public string? ProductName { get; set; }
    public int AvailableQuantity { get; set; }
}
