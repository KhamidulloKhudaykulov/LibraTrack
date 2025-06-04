using AdminPanel.Api.Extensions;
using AdminPanel.Api.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddControllers();

builder.Services.AddAuthorization();

builder.Services.AddSwaggerGen();

builder.Services.AddServices(builder.Configuration);

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(8085);
});

var app = builder.Build();


app.MapOpenApi();
app.UseSwagger();
app.UseSwaggerUI();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

app.UseHttpsRedirection();

app.UseCors("AllowLocalhost3000");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
