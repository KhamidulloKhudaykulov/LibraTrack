//using MediatR;
//using Microsoft.EntityFrameworkCore;
//using Newtonsoft.Json;
//using RentalService.Application.Common;
//using RentalService.Domain.Primitives;
//using RentalService.Persistence;
//using RentalService.Persistence.Outbox;

//namespace RentalService.Infrastructure.BackgroundJobs;

//public class ProcessOutboxMessagesJob : IProcessOutboxMessagesJob
//{
//    private readonly ApplicationDbContext _context;
//    private readonly IPublisher _publisher;

//    public ProcessOutboxMessagesJob(ApplicationDbContext context, IPublisher publisher)
//    {
//        _context = context;
//        _publisher = publisher;
//    }

//    public async Task ProcessAsync(CancellationToken token)
//    {
//        List<OutboxMessage> messages = await _context
//            .Set<OutboxMessage>()
//                .Where(m => m.ProcessedOnUtc == null)
//                .Take(10)
//                .ToListAsync(token);

//        foreach (var outboxMessage in messages)
//        {
//            IDomainEvent? domainEvent = JsonConvert
//                .DeserializeObject<DomainEvent>(outboxMessage.Content, new JsonSerializerSettings
//                {
//                    TypeNameHandling = TypeNameHandling.All
//                });

//            if (domainEvent == null)
//                continue;

//            await _publisher.Publish((INotification)domainEvent, token);

//            outboxMessage.ProcessedOnUtc = DateTime.UtcNow;
//        }

//        await _context.SaveChangesAsync(token);
//    }
//}
