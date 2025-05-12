using MediatR;

namespace NotificationService.Domain.Events.Users;

public class UserRegisteredDomainEvent : INotification
{
    public Guid UserId { get; set; }
    public string Email { get; set; } = default!;

    public UserRegisteredDomainEvent(Guid userId, string email)
    {
        UserId = userId;
        Email = email;
    }

}
