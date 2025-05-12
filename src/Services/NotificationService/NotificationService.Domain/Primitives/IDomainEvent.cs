using MediatR;

namespace NotificationService.Domain.Primitives;

public interface IDomainEvent : INotification
{
    DateTime OccurredOnUtc { get; }
}
