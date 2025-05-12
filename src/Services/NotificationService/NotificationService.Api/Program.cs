using Hangfire;
using NotificationService.Application.Extensions;
using NotificationService.Infrastructure.Extensions;
using NotificationService.Persistence.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddPersistence(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseHangfireDashboard();

app.UseBackgroundJobs();

app.Run();
