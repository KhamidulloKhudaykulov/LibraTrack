using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NotificationService.Domain.Events.Rentals;
using NotificationService.Infrastructure.MessageBroker.Configurations;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace NotificationService.Infrastructure.MessageBroker.RentalService.Consumers;

public class RentClosedConsumer
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly string _queueName;
    private readonly IOptions<RabbitMQSettiings> _settings;

    private readonly IServiceScopeFactory _serviceScopeFactory; // because RentGeneratedEventHandler is singleton
    public RentClosedConsumer(IServiceScopeFactory serviceScopeFactory, IOptions<RabbitMQSettiings> settings)
    {
        _settings = settings;
        var factory = new ConnectionFactory { HostName = _settings.Value.Host };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _queueName = _settings.Value.Queues["RentalClosed"]!;

        _channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task ConsumeAsync()
    {
        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            Console.WriteLine($"Received message: {message}");

            try
            {
                var rentClosedEvent = JsonConvert.DeserializeObject<RentClosedDomainEvent>(message);
                if (rentClosedEvent == null)
                {
                    Console.WriteLine("Deserialization failed.");
                    _channel.BasicNack(ea.DeliveryTag, false, true);
                    return;
                }

                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var publisher = scope.ServiceProvider.GetRequiredService<IPublisher>();

                    await publisher.Publish((INotification)rentClosedEvent);
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

    public void Close()
    {
        if (_channel.IsOpen)
        {
            _channel.Close();
        }
        if (_connection.IsOpen)
        {
            _connection.Close();
        }
    }
}
