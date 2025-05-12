
namespace NotificationService.Domain.Primitives;

public class DomainEvent : IDomainEvent
{
    public DateTime OccurredOnUtc => DateTime.UtcNow;
}
