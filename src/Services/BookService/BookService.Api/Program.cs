using BookService.Api.Extensions;
using BookService.Application;
using BookService.Infrastructure;
using BookService.Infrastructure.Grpc.Services;
using BookService.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Services.AddServices();

builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure();
builder.Services.AddPersistence();
builder.Services.AddApplication();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(8082);
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

app.MapControllers();

app.MapGrpcService<BookGrpcServiceClient>();

app.Run();
