namespace AccountService.Domain.Primitives;

public interface IDomainEvent
{
    DateTime OccuredOn { get; }
}
