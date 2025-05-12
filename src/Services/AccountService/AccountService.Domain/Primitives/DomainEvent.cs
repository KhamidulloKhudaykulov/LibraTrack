
namespace AccountService.Domain.Primitives;

public class DomainEvent : IDomainEvent
{
    public DateTime OccuredOn => DateTime.UtcNow;
}
