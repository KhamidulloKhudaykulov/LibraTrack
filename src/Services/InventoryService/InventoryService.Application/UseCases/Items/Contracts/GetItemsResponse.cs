namespace InventoryService.Application.UseCases.Items.Contracts;

public class GetItemsResponse
{
    public Guid Id { get; set; }
    public string? ProductName { get; set; }
    public int Amount { get; set; }
    public int AvailableQuantity { get; set; }
    public decimal Price { get; set; }
    public decimal TotalPrice { get; set; }
    public string? CreatedDate { get; set; }
}
