using MediatR;
using NotificationService.Application.Interfaces.Clients;
using NotificationService.Application.Services;
using NotificationService.Domain.Events.Rentals;

namespace NotificationService.Application.EventHandlers.Rentals;

public class RentGeneratedDomainEventHandler : INotificationHandler<RentGeneratedDomainEvent>
{
    private readonly IEmailService _emailService;
    private readonly IUserServiceClient _userServiceClient;
    private readonly IBookServiceClient _bookServiceClient;

    public RentGeneratedDomainEventHandler(IEmailService emailService, IUserServiceClient userServiceClient, IBookServiceClient bookServiceClient)
    {
        _emailService = emailService;
        _userServiceClient = userServiceClient;
        _bookServiceClient = bookServiceClient;
    }

    public async Task Handle(RentGeneratedDomainEvent notification, CancellationToken cancellationToken)
    {
        var email = await _userServiceClient.GetUserEmailAsync(notification.UserId.ToString());
        var bookName = await _bookServiceClient.GetBookNameAsync(notification.BookId.ToString());

        var subject = "Xizmatimizdan foydalanganingiz uchun tashakkur!";
        var body = $"Hurmatli foydalanuvchi, siz '{bookName}' kitobini muvaffaqiyatli ijaraga oldingiz! Qaytarish sanasi {notification.EndDate}";
        await _emailService.SendAsync(email, subject, body);
    }
}
