namespace BookService.Domain.Primitives;

public interface IDomainEvent
{
    DateTime OccurredOn { get; }
}
