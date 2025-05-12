using AccountService.Domain.Primitives;
using MediatR;

namespace AccountService.Domain.Events;

public sealed class UserRegisteredDomainEvent : DomainEvent, INotification
{
    public Guid UserId { get; set; }
    public string Email { get; set; }
    public UserRegisteredDomainEvent(Guid userId, string email)
    {
        UserId = userId;
        Email = email;
    }
}
