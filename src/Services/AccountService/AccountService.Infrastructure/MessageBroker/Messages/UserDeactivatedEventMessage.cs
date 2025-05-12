namespace AccountService.Infrastructure.MessageBroker.Messages;

public class UserDeactivatedEventMessage
{
    public Guid UserId { get; set; }
}
