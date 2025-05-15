using AdminPanel.Api.Application.Commands;
using AdminPanel.Api.Application.Requests;
using AdminPanel.Api.Persistence;
using AdminPanel.Api.Persistence.Repositories;
using AdminPanel.Api.Services;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.Api.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IPasswordService, PasswordService>();
        services.AddScoped<IAdminRepository, AdminRepository>();
        services.AddScoped<CreateAdminRequestHandler>();
        services.AddScoped<GetVerifiedAdminRequestHandler>();

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("Default"));
        });

        services.AddHttpClient("IdentityService", client =>
        {
            client.BaseAddress = new Uri("https://localhost:7029/api");
        });

        return services;
    }
}
