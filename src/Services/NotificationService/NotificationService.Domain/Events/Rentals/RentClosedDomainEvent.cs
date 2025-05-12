using NotificationService.Domain.Primitives;

namespace NotificationService.Domain.Events.Rentals;

public class RentClosedDomainEvent : DomainEvent
{
    public Guid RentId { get; set; }
    public Guid UserId { get; set; }
    public Guid BookId { get; set; }
    public RentClosedDomainEvent(Guid rentId, Guid userId, Guid bookId)
    {
        RentId = rentId;
        UserId = userId;
        BookId = bookId;
    }
}
