using AccountService.Domain.Primitives;
using MediatR;

namespace AccountService.Domain.Events;

public class UserDeactivatedDomainEvent : IDomainEvent, INotification
{
    public Guid UserId { get; set; }

    public DateTime OccuredOn => DateTime.UtcNow;

    public UserDeactivatedDomainEvent(Guid userId)
    {
        UserId = userId;
    }
}
