using AccountService.Infrastructure.MessageBroker.Configurations;
using AccountService.Infrastructure.MessageBroker.Messages;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace AccountService.Infrastructure.MessageBroker.Producers;

public class UserEventProducer
{
    private readonly IOptions<RabbitMQSettings> _settings;
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public UserEventProducer(IOptions<RabbitMQSettings> settings)
    {
        _settings = settings;
        var factory = new ConnectionFactory { HostName = _settings.Value.Host };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.QueueDeclare(queue: _settings.Value.Queues["UserRegistered"],
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

    }

    public void PublishUserRegistered(UserRegisteredEventMessage userRegisteredEventMessage)
    {
        var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(userRegisteredEventMessage));

        _channel.BasicPublish(exchange: "",
                             routingKey: _settings.Value.Queues["UserRegistered"],
                             basicProperties: null,
                             body: body);
    }

    public void PublishUserDeactivated(UserDeactivatedEventMessage userDeactivatedEventMessage)
    {
        var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(userDeactivatedEventMessage));

        _channel.BasicPublish(exchange: "",
                             routingKey: _settings.Value.Queues["UserDeactivated"],
                             basicProperties: null,
                             body: body);
    }

    public void Dispose()
    {
        _channel?.Close();
        _channel?.Dispose();
        _connection?.Close();
        _connection?.Dispose();
    }
}
