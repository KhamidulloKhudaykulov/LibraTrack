using MediatR;
using NotificationService.Application.Interfaces.Clients;
using NotificationService.Application.Services;
using NotificationService.Domain.Events.Rentals;

namespace NotificationService.Application.EventHandlers.Rentals;

public class RentClosedDomainEventHandler : INotificationHandler<RentClosedDomainEvent>
{
    private readonly IEmailService _emailService;
    private readonly IUserServiceClient _userServiceClient;
    private readonly IBookServiceClient _bookServiceClient;

    public RentClosedDomainEventHandler(IEmailService emailService, IUserServiceClient userServiceClient, IBookServiceClient bookServiceClient)
    {
        _emailService = emailService;
        _userServiceClient = userServiceClient;
        _bookServiceClient = bookServiceClient;
    }

    public async Task Handle(RentClosedDomainEvent notification, CancellationToken cancellationToken)
    {
        var email = await _userServiceClient.GetUserEmailAsync(notification.UserId.ToString());
        var bookName = await _bookServiceClient.GetBookNameAsync(notification.BookId.ToString());

        var subject = "Xizmatimizdan foydalanganingiz uchun tashakkur!";
        var body = $"Hurmatli foydalanuvchi, siz '{bookName}' kitobini muvaffaqiyatli topshirdingiz. Xizmatimizdan foydalanganingiz uchun tashakkur!";
        await _emailService.SendAsync(email, subject, body);
    }
}
