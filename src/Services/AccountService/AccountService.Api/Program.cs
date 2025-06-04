using AccountService.Api.Extensions;
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

builder.Services.AddServices();

//builder.Services.AddAuthorization();

builder.Services.AddApplication();
builder.Services.AddPersistence();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default"));
});

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(8080);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

//app.UseHttpsRedirection();

app.UseHangfireDashboard();

app.MapGrpcService<AccountGrpcServiceHandler>();

//app.UseAuthentication();

//app.UseAuthorization();

app.UseBackgoundJob();

app.MapControllers();

app.Run();
