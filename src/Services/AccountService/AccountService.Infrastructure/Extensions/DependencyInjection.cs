using AccountService.Infrastructure.BackgroundJobs;
using AccountService.Infrastructure.MessageBroker.Configurations;
using AccountService.Infrastructure.MessageBroker.Producers;
using AccountService.Persistence.Interceptors;
using Hangfire;
using Hangfire.PostgreSql;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AccountService.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHangfire(config =>
            config.UsePostgreSqlStorage(options =>
            {
                options.UseNpgsqlConnection(configuration.GetConnectionString("Default"));
            }));

        services.AddHangfireServer();

        services.AddGrpc();

        services.Configure<RabbitMQSettings>(
            configuration.GetSection("RabbitMqSettings"));

        services.AddScoped<UsersOutboxMessagesJob>();
        services.AddSingleton<UserEventProducer>();

        services.AddTransient(
            typeof(IPipelineBehavior<,>),
            typeof(ConvertDomainEventsToOutboxMessagesInterceptor<,>));

        return services;
    }
}
