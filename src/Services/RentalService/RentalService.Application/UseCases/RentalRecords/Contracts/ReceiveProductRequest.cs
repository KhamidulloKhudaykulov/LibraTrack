namespace RentalService.Application.UseCases.RentalRecords.Contracts;

public class ReceiveProductRequest
{
    public Guid ProductId { get; set; }
    public int Amount { get; set; }
}
