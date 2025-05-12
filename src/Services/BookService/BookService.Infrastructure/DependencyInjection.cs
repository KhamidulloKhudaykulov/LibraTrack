using Microsoft.Extensions.DependencyInjection;

namespace BookService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddGrpc();

        return services;
    }
}
