using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RentalService.Domain.Repositories;
using RentalService.Persistence.Interceptors;
using RentalService.Persistence.Repositories;

namespace RentalService.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IRentalRecordRepository, RentalRecordRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddTransient(
            typeof(IPipelineBehavior<,>),
            typeof(ConvertDomainEventsToOutboxMessagesInterceptor<,>));


        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("Default")));

        return services;
    }
}
