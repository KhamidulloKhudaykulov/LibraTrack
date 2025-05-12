namespace RentalService.Infrastructure.MessageBroker.Messages;

public class RentClosedEventMessage
{
    public Guid UserId { get; set; }
    public Guid BookId { get; set; }
}
