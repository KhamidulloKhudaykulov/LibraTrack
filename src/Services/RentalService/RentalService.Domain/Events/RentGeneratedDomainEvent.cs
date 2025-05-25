using RentalService.Domain.Primitives;

namespace RentalService.Domain.Events;

public class RentGeneratedDomainEvent : DomainEvent
{
    public Guid UserId { get; set; }
    public Guid BookId { get; set; }
    public string StartDate { get; set; } = default!;
    public string EndDate { get; set; } = default!;
    public Decimal RentPrice { get; set; }

    public RentGeneratedDomainEvent(Guid bookId, Guid userId, string startDate, string endDate, decimal rentPrice)
    {
        BookId = bookId;
        UserId = userId;
        StartDate = startDate;
        EndDate = endDate;
        RentPrice = rentPrice;
    }
}
