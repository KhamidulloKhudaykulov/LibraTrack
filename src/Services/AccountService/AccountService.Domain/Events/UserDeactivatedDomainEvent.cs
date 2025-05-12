using AccountService.Domain.Primitives;
using MediatR;

namespace AccountService.Domain.Events;

public class UserDeactivatedDomainEvent : DomainEvent, INotification
{
    public Guid UserId { get; set; }

    public UserDeactivatedDomainEvent(Guid userId)
    {
        UserId = userId;
    }
}
