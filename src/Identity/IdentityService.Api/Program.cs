using IdentityService.Api.Authentication;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddControllers();

builder.Services.AddScoped<IJwtService, JwtService>();

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(8086);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
