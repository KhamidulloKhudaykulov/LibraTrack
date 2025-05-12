using AccountService.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AccountService.Application.UseCases.Users.Events;

public class UserRegisteredDomainEventHandler : INotificationHandler<UserRegisteredDomainEvent>
{
    private readonly ILogger<UserRegisteredDomainEventHandler> _logger;

    public UserRegisteredDomainEventHandler(ILogger<UserRegisteredDomainEventHandler> logger)
    {
        _logger = logger;
    }

    public async Task Handle(UserRegisteredDomainEvent notification, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }
}
