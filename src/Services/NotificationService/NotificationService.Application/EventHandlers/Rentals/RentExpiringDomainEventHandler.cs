using MediatR;
using NotificationService.Application.Interfaces.Clients;
using NotificationService.Application.Services;
using NotificationService.Domain.Events.Rentals;

namespace NotificationService.Application.EventHandlers.Rentals;

public class RentExpiringDomainEventHandler : INotificationHandler<RentExpiringDomainEvent>
{
    private readonly IEmailService _emailService;
    private readonly IUserServiceClient _userServiceClient;
    private readonly IBookServiceClient _bookServiceClient;

    public RentExpiringDomainEventHandler(IEmailService emailService, IUserServiceClient userServiceClient, IBookServiceClient bookServiceClient)
    {
        _emailService = emailService;
        _userServiceClient = userServiceClient;
        _bookServiceClient = bookServiceClient;
    }

    public async Task Handle(RentExpiringDomainEvent notification, CancellationToken cancellationToken)
    {
        var email = await _userServiceClient.GetUserEmailAsync(notification.UserId.ToString());
        var bookName = await _bookServiceClient.GetBookNameAsync(notification.BookId.ToString());

        var subject = "Maxsulotni qaytarish sanasiga 1 kun qoldi!";
        var body = $"Hurmatli foydalanuvchi, eslatib o'tamiz, siz ijaraga olgan '{bookName}' maxsulotimizni qaytarish sanasi {notification.CloseDate}";
        await _emailService.SendAsync(email, subject, body);
    }
}
