namespace RentalService.Infrastructure.MessageBroker.Configurations;

public class RabbitMQSettiings
{
    public string Host { get; set; } = default!;
    public Dictionary<string, string> Queues { get; set; } = default!;
}
