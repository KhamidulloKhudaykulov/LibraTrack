using MediatR;
using RentalService.Application.Interfaces.Clients;
using RentalService.Application.Services;
using RentalService.Domain.Events;
using RentalService.Domain.Repositories;

namespace RentalService.Application.UseCases.RentalRecords.Events;

public class RentClosedDomainEventHandler : INotificationHandler<RentClosedDomainEvent>
{
    private readonly IEmailService _emailService;
    private readonly IRentalRecordRepository _recordRepository;
    private readonly IUserServiceClient _userServiceClient;
    private readonly IBookServiceClient _bookServiceClient;
    public RentClosedDomainEventHandler(
        IEmailService emailService,
        IRentalRecordRepository recordRepository,
        IUserServiceClient userServiceClient,
        IBookServiceClient bookServiceClient)
    {
        _emailService = emailService;
        _recordRepository = recordRepository;
        _userServiceClient = userServiceClient;
        _bookServiceClient = bookServiceClient;
    }

    public async Task Handle(RentClosedDomainEvent notification, CancellationToken cancellationToken)
    {
        //var rent = await _recordRepository.SelectAsync(r => r.Id == notification.RentId);
        //var email = await _userServiceClient.GetUserEmailAsync(rent.UserId.ToString());
        //var bookName = await _bookServiceClient.GetBookNameAsync(rent.BookId.ToString());

        //var to = email;
        //var subject = "Xizmatimizdan foydalanganingiz uchun tashakkur!";
        //var body = $"Hurmatli foydalanuvchi, siz '{bookName}' kitobini muvaffaqiyatli Topshirdingiz! Xizmatimizdan foydalanganingiz uchun tashakkur";
        //await _emailService.SendAsync(to, subject, body);

        await Task.CompletedTask;
    }
}
