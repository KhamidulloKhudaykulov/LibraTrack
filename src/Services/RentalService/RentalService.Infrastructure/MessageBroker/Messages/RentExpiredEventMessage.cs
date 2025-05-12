namespace RentalService.Infrastructure.MessageBroker.Messages;

public class RentExpiredEventMessage
{
    public Guid UserId { get; set; }
    public Guid BookId { get; set; }
    public DateTime CloseDate { get; set; }
}
