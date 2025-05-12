
using AccountService.Domain.Events;
using AccountService.Domain.Primitives;
using AccountService.Infrastructure.MessageBroker.Messages;
using AccountService.Infrastructure.MessageBroker.Producers;
using AccountService.Persistence;
using AccountService.Persistence.Outbox;
using Hangfire;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace AccountService.Infrastructure.BackgroundJobs;

public class UsersOutboxMessagesJob
{
    private readonly ApplicationDbContext _context;
    private readonly UserEventProducer _userEventProducer;
    private readonly IPublisher _publisher;

    public UsersOutboxMessagesJob(ApplicationDbContext context, IPublisher publisher, UserEventProducer userEventProducer)
    {
        _context = context;
        _publisher = publisher;
        _userEventProducer = userEventProducer;
    }

    [JobDisplayName("Process Users Outbox Messages")]
    public async Task ExecuteAsync(CancellationToken token)
    {
        List<OutboxMessage> outboxMessages = await _context
            .Set<OutboxMessage>()
            .Take(20)
            .Where(o => o.ProcessedOnUtc == null)
            .ToListAsync();

        foreach (var outboxMessage in outboxMessages)
        {
            IDomainEvent? domainEvent = JsonConvert
                .DeserializeObject<DomainEvent>(
                    outboxMessage.Content, new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All,
                    });

            if (domainEvent == null)
                continue;

            if (domainEvent is UserRegisteredDomainEvent userRegisteredDomainEvent)
            {
                await _publisher.Publish((INotification)userRegisteredDomainEvent, token);
                _userEventProducer.PublishUserRegistered(new UserRegisteredEventMessage
                {
                    UserId = userRegisteredDomainEvent.UserId,
                    Email = userRegisteredDomainEvent.Email
                });
            }

            outboxMessage.ProcessedOnUtc = DateTime.UtcNow;
        }

        await _context.SaveChangesAsync();
    }
}
