using MediatR;
using RentalService.Application.Interfaces.Clients;
using RentalService.Application.Services;
using RentalService.Domain.Events;

namespace RentalService.Application.UseCases.RentalRecords.Events;

public class RentGeneratedDomainEventHandler : INotificationHandler<RentGeneratedDomainEvent>
{
    private readonly IUserServiceClient _userServiceClient;
    private readonly IBookServiceClient _bookServiceClient;
    private readonly IEmailService _emailService;

    public RentGeneratedDomainEventHandler(IUserServiceClient userServiceClient, IBookServiceClient bookServiceClient, IEmailService emailService)
    {
        _userServiceClient = userServiceClient;
        _bookServiceClient = bookServiceClient;
        _emailService = emailService;
    }

    public async Task Handle(RentGeneratedDomainEvent notification, CancellationToken cancellationToken)
    {
        //var email = await _userServiceClient.GetUserEmailAsync(notification.UserId.ToString());
        //var bookName = await _bookServiceClient.GetBookNameAsync(notification.BookId.ToString());

        //var to = email;
        //var subject = "Xizmatimizdan foydalanganingiz uchun tashakkur!";
        //var body = $"Hurmatli foydalanuvchi, siz '{bookName}' kitobini muvaffaqiyatli ijaraga oldingiz! Qaytarish sanasi {notification.EndDate}";
        //await _emailService.SendAsync(to, subject, body);

        await Task.CompletedTask;
    }
}
