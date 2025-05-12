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
            .AddOrUpdate<RentGeneratedConsumer>(
                "rent-generated-consumer",
                job => job.ConsumeAsync(),
                "*/10 * * * * *");

        app.Services
            .GetRequiredService<IRecurringJobManager>()
            .AddOrUpdate<UserRegisteredConsumer>(
                "rent-generated-consumer",
                job => job.ConsumeAsync(),
                "*/10 * * * * *");

        //app.Services
        //    .GetRequiredService<IRecurringJobManager>()
        //    .AddOrUpdate<RentClosedConsumer>(
        //        "rent-closed-consumer",
        //        job => job.ConsumeAsync(),
        //        "*/1 * * * *");

        return app;
    }
}
