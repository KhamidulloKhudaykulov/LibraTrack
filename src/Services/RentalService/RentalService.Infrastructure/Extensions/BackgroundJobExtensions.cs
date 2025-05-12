using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using RentalService.Application.Common;

namespace RentalService.Infrastructure.Extensions;

public static class BackgroundJobExtensions
{
    public static IApplicationBuilder UseBackgroundJobs(this WebApplication app)
    {
        //app.Services
        //    .GetRequiredService<IRecurringJobManager>()
        //    .AddOrUpdate<IProcessOutboxMessagesJob>(
        //        "outbox-processor",
        //        job => job.ProcessAsync(CancellationToken.None),
        //        "*/1 * * * *");

        app.Services
            .GetRequiredService<IRecurringJobManager>()
            .AddOrUpdate<IRentalOutboxProcessorJob>(
                "rentals-outbox-processor",
                job => job.ProcessAsync(CancellationToken.None),
                "*/1 * * * *");

        return app;
    }
}
