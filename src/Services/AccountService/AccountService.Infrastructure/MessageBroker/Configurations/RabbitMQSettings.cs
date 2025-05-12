namespace AccountService.Infrastructure.MessageBroker.Configurations;

public class RabbitMQSettings
{
    public string Host { get; set; } = default!;
    public Dictionary<string, string> Queues { get; set; } = default!;
}
