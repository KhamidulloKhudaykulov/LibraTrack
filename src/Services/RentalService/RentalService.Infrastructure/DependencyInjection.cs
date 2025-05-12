using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RentalService.Application.Common;
using RentalService.Application.Interfaces.Clients;
using RentalService.Application.Services;
using RentalService.Infrastructure.BackgroundJobs;
using RentalService.Infrastructure.Grpc.Clients;
using RentalService.Infrastructure.MessageBroker.Configurations;
using RentalService.Infrastructure.MessageBroker.Producers;
using RentalService.Infrastructure.Services;

namespace RentalService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUserServiceClient, UserServiceClient>();
        services.AddScoped<IBookServiceClient, BookServiceClient>();
        services.AddScoped<IEmailService, EmailService>();

        services.AddScoped<IRentalOutboxProcessorJob, RentalsOutboxProcessorJob>();

        services.Configure<RabbitMQSettiings>(
            configuration.GetSection("RabbitMqSettings"));

        services.AddSingleton<RentalEventProducer>();

        services.AddHangfire(config =>
            config.UsePostgreSqlStorage(options =>
            {
                options.UseNpgsqlConnection(configuration.GetConnectionString("Default"));
            }));

        services.AddHangfireServer();

        return services;
    }
}
