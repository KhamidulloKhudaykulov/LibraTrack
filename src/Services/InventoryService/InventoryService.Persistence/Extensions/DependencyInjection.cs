using InventoryService.Domain.Repositories;
using InventoryService.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryService.Persistence.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddScoped<IItemRepository, ItemRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
