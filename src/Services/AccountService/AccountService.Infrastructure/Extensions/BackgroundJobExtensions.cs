using AccountService.Infrastructure.BackgroundJobs;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace AccountService.Infrastructure.Extensions;

public static class BackgroundJobExtensions
{
    public static IApplicationBuilder UseBackgoundJob(this WebApplication app)
    {
        app.Services
            .GetRequiredService<IRecurringJobManager>()
            .AddOrUpdate<UsersOutboxMessagesJob>(
                recurringJobId: "ProcessUsersOutboxMessages",
                methodCall: job => job.ExecuteAsync(CancellationToken.None),
                cronExpression: "*/10 * * * * *");
        return app;
    }
}
