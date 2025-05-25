namespace RentalService.Application.UseCases.RentalRecords.Contracts;

public class RentResultResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid BookId { get; set; }
    public string UserName { get; set; } = default!;
    public string BookTitle { get; set; } = default!;
    public decimal Price { get; set; }
    public bool IsReturned { get; set; }
    public bool IsPayed { get; set; }
    public bool IsDeleted { get; set; }
    public string StartDate { get; set; } = default!;
    public string EndDate { get; set; } = default!;
}
