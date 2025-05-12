using MediatR;

namespace NotificationService.Domain.Events.Rentals;

public class RentExpiringDomainEvent : INotification
{
    public Guid UserId { get; set; }
    public Guid BookId { get; set; }
    public DateTime CloseDate { get; set; }
}
