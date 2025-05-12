namespace AccountService.Infrastructure.MessageBroker.Messages;

public class UserRegisteredEventMessage
{
    public Guid UserId { get; set; }
    public string Email { get; set; } = default!;
}
