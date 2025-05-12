using MediatR;

namespace NotificationService.Domain.Events.Users;

public class UserDeactivatedDomainEvent : INotification
{
    public Guid UserId { get; set; }
}
