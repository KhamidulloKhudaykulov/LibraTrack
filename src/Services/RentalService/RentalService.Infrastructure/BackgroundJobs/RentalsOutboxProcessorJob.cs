using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RentalService.Application.Common;
using RentalService.Domain.Events;
using RentalService.Domain.Primitives;
using RentalService.Infrastructure.MessageBroker.Messages;
using RentalService.Infrastructure.MessageBroker.Producers;
using RentalService.Persistence;
using RentalService.Persistence.Outbox;

namespace RentalService.Infrastructure.BackgroundJobs;

public class RentalsOutboxProcessorJob : IRentalOutboxProcessorJob
{
    private readonly ApplicationDbContext _context;
    private readonly RentalEventProducer _rentalEventProducer;
    private readonly IPublisher _publisher;

    public RentalsOutboxProcessorJob(ApplicationDbContext context, IPublisher publisher, RentalEventProducer rentalEventProducer)
    {
        _context = context;
        _publisher = publisher;
        _rentalEventProducer = rentalEventProducer;
    }
    public async Task ProcessAsync(CancellationToken token)
    {
        List<OutboxMessage> messages = await _context
            .Set<OutboxMessage>()
                .Where(m => m.ProcessedOnUtc == null)
                .Take(10)
                .ToListAsync(token);

        foreach (var outboxMessage in messages)
        {
            IDomainEvent? domainEvent = JsonConvert
                .DeserializeObject<DomainEvent>(outboxMessage.Content, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                });

            if (domainEvent == null)
                continue;

            if (domainEvent is RentGeneratedDomainEvent rentGeneratedDomainEvent)
            {
                await _publisher.Publish((INotification)domainEvent, token);
                _rentalEventProducer.PublishRentalCreated(new RentCreatedEventMessage
                {
                    UserId = rentGeneratedDomainEvent.UserId,
                    BookId = rentGeneratedDomainEvent.BookId,
                    StartDate = rentGeneratedDomainEvent.StartDate,
                    EndDate = rentGeneratedDomainEvent.EndDate
                });

                outboxMessage.ProcessedOnUtc = DateTime.UtcNow;
            }

            if (domainEvent is RentClosedDomainEvent rentClosedDomainEvent)
            {
                await _publisher.Publish((INotification)domainEvent, token);
                _rentalEventProducer.PublishRentalClosed(new RentClosedEventMessage
                {
                    UserId = rentClosedDomainEvent.UserId,
                    BookId = rentClosedDomainEvent.BookId
                });

                outboxMessage.ProcessedOnUtc = DateTime.UtcNow;
            }
        }

        await _context.SaveChangesAsync(token);
    }
}
