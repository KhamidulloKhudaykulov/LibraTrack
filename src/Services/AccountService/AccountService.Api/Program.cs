using AccountService.Api.Logging;
using AccountService.Application;
using AccountService.Infrastructure.Extensions;
using AccountService.Infrastructure.Grpc.Services;
using AccountService.Persistence;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Serilog;

LoggingConfiguration.Configure();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

builder.Services.AddOpenApi();

builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
builder.Services.AddPersistence();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default"));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.UseHangfireDashboard();

app.MapGrpcService<AccountGrpcServiceHandler>();

app.UseBackgoundJob();

app.Run();
