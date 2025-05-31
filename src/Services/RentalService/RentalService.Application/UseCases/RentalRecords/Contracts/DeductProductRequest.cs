namespace RentalService.Application.UseCases.RentalRecords.Contracts;

public class DeductProductRequest
{
    public Guid ProductId { get; set; }
    public int Amount { get; set; }
}
