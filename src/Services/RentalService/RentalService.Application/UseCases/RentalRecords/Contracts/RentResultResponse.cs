namespace RentalService.Application.UseCases.RentalRecords.Contracts;

public class RentResultResponse
{
    public Guid UserId { get; set; }
    public Guid BookId { get; set; }
    public string UserEmail { get; set; } = default!;
    public string BookTitle { get; set; } = default!;
    public string StartDate { get; set; } = default!;
    public string EndDate { get; set; } = default!;
}
