using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using NotificationService.Infrastructure.MessageBroker.AccountService.Consumers;
using NotificationService.Infrastructure.MessageBroker.RentalService.Consumers;

namespace NotificationService.Infrastructure.Extensions;

public static class BackgroundJobExtensions
{
    public static IApplicationBuilder UseBackgroundJobs(this WebApplication app)
    {
        app.Services
            .GetRequiredService<IRecurringJobManager>()
            .AddOrUpdate<RentExpiringConsumer>(
                "rent-expiring-consumer",
                job => job.ConsumeAsync(CancellationToken.None),
                Cron.Daily());

        return app;
    }
}
