using MediatR;
using NotificationService.Application.Services;
using NotificationService.Domain.Events.Users;

namespace NotificationService.Application.EventHandlers.Users;

public class UserRegisteredDomainEventHandler : INotificationHandler<UserRegisteredDomainEvent>
{
    private readonly IEmailService _emailService;

    public UserRegisteredDomainEventHandler(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public async Task Handle(UserRegisteredDomainEvent notification, CancellationToken cancellationToken)
    {
        var subject = "Ro'yxatdan o'tildi!";
        var body = $"Hurmatli foydalanuvchi, LibraTrack loyihasiga muvaffaqiyatli a'zo bo'ldingiz!";
        await _emailService.SendAsync(notification.Email, subject, body);
    }
}
