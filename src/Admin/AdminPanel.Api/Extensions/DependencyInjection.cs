using AdminPanel.Api.Application.Commands;
using AdminPanel.Api.Application.Requests;
using AdminPanel.Api.Persistence;
using AdminPanel.Api.Persistence.Repositories;
using AdminPanel.Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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


        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes("this_is_a_very_long_secret_key_with_32_chars!"))
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies["access-token"];
                        return Task.CompletedTask;
                    }
                };
            });



        return services;
    }
}
