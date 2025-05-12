using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NotificationService.Domain.Events.Users;
using NotificationService.Infrastructure.MessageBroker.Configurations;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace NotificationService.Infrastructure.MessageBroker.AccountService.Consumers;

public class UserRegisteredConsumer
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly IServiceScopeFactory _scopeFactory;

    private readonly IOptions<RabbitMQSettiings> _settings;
    private readonly string _queueName;

    public UserRegisteredConsumer(IServiceScopeFactory scopeFactory, IOptions<RabbitMQSettiings> settings)
    {
        _scopeFactory = scopeFactory;
        _settings = settings;

        _queueName = _settings.Value.Queues["UserRegistered"];

        var factory = new ConnectionFactory { HostName = _settings.Value.Host };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.QueueDeclare(
                            queue: _queueName,
                            durable: false,
                            autoDelete: false,
                            exclusive: false,
                            arguments: null);
    }

    public async Task ConsumeAsync()
    {
        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += async (sender, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            Console.WriteLine($"Received message: {message}");

            try
            {
                var userRegisteredEvent = JsonConvert.DeserializeObject<UserRegisteredDomainEvent>(message);
                if (userRegisteredEvent == null)
                {
                    Console.WriteLine("Deserialization failed.");
                    _channel.BasicNack(ea.DeliveryTag, false, true);
                    return;
                }

                using (var scope = _scopeFactory.CreateScope())
                {
                    var publisher = scope.ServiceProvider.GetRequiredService<IPublisher>();

                    await publisher.Publish((INotification)userRegisteredEvent);
                }

                _channel.BasicAck(ea.DeliveryTag, false);
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Deserialization error: {ex.Message}");
                _channel.BasicNack(ea.DeliveryTag, false, true);
            }
        };

        _channel.BasicConsume(queue: _queueName, autoAck: false, consumer: consumer);

        await Task.CompletedTask;
    }
}
