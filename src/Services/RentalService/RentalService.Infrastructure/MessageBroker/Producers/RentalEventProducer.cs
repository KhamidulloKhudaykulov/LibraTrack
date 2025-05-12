using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RentalService.Infrastructure.MessageBroker.Configurations;
using RentalService.Infrastructure.MessageBroker.Messages;
using System.Text;

namespace RentalService.Infrastructure.MessageBroker.Producers;

public class RentalEventProducer
{
    private readonly IOptions<RabbitMQSettiings> _settings;

    public RentalEventProducer(IOptions<RabbitMQSettiings> settings)
    {
        _settings = settings;
    }

    public void PublishRentalCreated(RentCreatedEventMessage rentEvent)
    {
        var factory = new ConnectionFactory() { HostName = _settings.Value.Host };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue: _settings.Value.Queues["RentalCreated"],
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

        var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(rentEvent));

        channel.BasicPublish(exchange: "",
                             routingKey: _settings.Value.Queues["RentalCreated"],
                             basicProperties: null,
                             body: body);
    }

    public void PublishRentalClosed(RentClosedEventMessage rentEvent)
    {
        var factory = new ConnectionFactory() { HostName = _settings.Value.Host };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue: _settings.Value.Queues["RentalClosed"],
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

        var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(rentEvent));

        channel.BasicPublish(exchange: "",
                             routingKey: _settings.Value.Queues["RentalClosed"],
                             basicProperties: null,
                             body: body);
    }

    public void PublishExpiredRents(List<RentExpiredEventMessage> rentEvents)
    {
        var factory = new ConnectionFactory() { HostName = _settings.Value.Host };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue: _settings.Value.Queues["RentalExpiring"],
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

        var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(rentEvents));

        channel.BasicPublish(exchange: "",
                             routingKey: _settings.Value.Queues["RentalExpiring"],
                             basicProperties: null,
                             body: body);
    }
}
