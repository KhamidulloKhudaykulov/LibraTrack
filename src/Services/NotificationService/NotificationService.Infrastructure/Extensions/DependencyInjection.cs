using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NotificationService.Application.Interfaces.Clients;
using NotificationService.Application.Services;
using NotificationService.Infrastructure.Grpc.Clients;
using NotificationService.Infrastructure.MessageBroker.AccountService.Consumers;
using NotificationService.Infrastructure.MessageBroker.Configurations;
using NotificationService.Infrastructure.MessageBroker.RentalService.Consumers;
using NotificationService.Infrastructure.Services;

namespace NotificationService.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IUserServiceClient, UserServiceClient>();
        services.AddScoped<IBookServiceClient, BookServiceClient>();

        services.AddSingleton<RentGeneratedConsumer>();
        services.AddSingleton<RentExpiringConsumer>();
        services.AddSingleton<UserRegisteredConsumer>();

        services.AddHostedService<UserDeactivatedConsumer>();
        services.AddHostedService<UserRegisteredConsumer>();
        //services.AddSingleton<RentClosedConsumer>();

        services.AddHangfire(config =>
            config.UsePostgreSqlStorage(options =>
            {
                options.UseNpgsqlConnection(configuration.GetConnectionString("Default"));
            }));

        services.Configure<RabbitMQSettiings>(
            configuration.GetSection("RabbitMqSettings"));

        services.AddHangfireServer();

        return services;
    }
}
