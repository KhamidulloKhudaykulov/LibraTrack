
namespace BookService.Domain.Primitives;

public class DomainEvent : IDomainEvent
{
    public DateTime OccurredOn => DateTime.UtcNow;
}
