using AccountService.Domain.Repositories;
using AccountService.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace AccountService.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
        services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

        return services;
    }
}
