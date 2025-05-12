using MediatR;

namespace RentalService.Domain.Primitives;

public interface IDomainEvent : INotification
{
    DateTime OccurredOn { get; }
}
