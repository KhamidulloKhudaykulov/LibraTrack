using AdminPanel.Api.Application.Commands;
using AdminPanel.Api.Persistence;
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
        services.AddSingleton<CreateAdminRequestHandler>();

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("Default"));
        });

        return services;
    }
}
