using MediatR;
using NotificationService.Application.Interfaces.Clients;
using NotificationService.Application.Services;
using NotificationService.Domain.Events.Users;

namespace NotificationService.Application.EventHandlers.Users;

public class UserDeactivatedDomainEventHandler : INotificationHandler<UserDeactivatedDomainEvent>
{
    private readonly IEmailService _emailService;
    private readonly IUserServiceClient _userServiceClient;

    public UserDeactivatedDomainEventHandler(IEmailService emailService, IUserServiceClient userServiceClient)
    {
        _emailService = emailService;
        _userServiceClient = userServiceClient;
    }

    public async Task Handle(UserDeactivatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var userEmail = await _userServiceClient.GetUserEmailAsync(notification.UserId.ToString());
        if (userEmail is null)
        {
            Console.WriteLine("User is not found");
            return;
        }


        var subject = "Foydalanuvchi bloklandi!";
        var body = $"Hurmatli foydalanuvchi, siz LibraTrack loyihasidan chetlashtirildingiz!";
        await _emailService.SendAsync(userEmail, subject, body);
    }
}
