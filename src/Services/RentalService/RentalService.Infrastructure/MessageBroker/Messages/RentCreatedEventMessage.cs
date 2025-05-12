namespace RentalService.Infrastructure.MessageBroker.Messages;

public class RentCreatedEventMessage
{
    public Guid UserId { get; set; }
    public Guid BookId { get; set; }
    public string StartDate { get; set; } = default!;
    public string EndDate { get; set; } = default!;
}
