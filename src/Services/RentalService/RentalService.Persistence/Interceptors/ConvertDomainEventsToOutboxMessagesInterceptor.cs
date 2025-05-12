using MediatR;
using Newtonsoft.Json;
using RentalService.Domain.Primitives;
using RentalService.Persistence.Outbox;

namespace RentalService.Persistence.Interceptors;

public class ConvertDomainEventsToOutboxMessagesInterceptor<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly ApplicationDbContext _context;

    public ConvertDomainEventsToOutboxMessagesInterceptor(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var response = await next();

        var domainEvents = _context.ChangeTracker
            .Entries<Entity>()
            .SelectMany(e => e.Entity.GetDomainEvents())
            .ToList();

        foreach (var domainEvent in domainEvents)
        {
            var outboxMessage = new OutboxMessage
            {
                Id = Guid.NewGuid(),
                OccurredOnUtc = DateTime.UtcNow,
                Type = domainEvent.GetType().Name,
                Content = JsonConvert.SerializeObject(
                    domainEvent,
                    new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All,
                    })
            };

            await _context.Set<OutboxMessage>().AddAsync(outboxMessage);
        }

        await _context.SaveChangesAsync();

        return response;
    }
}
