using InventoryService.Application.Interfaces.Clients;
using Microsoft.Extensions.DependencyInjection;
using NotificationService.Infrastructure.Grpc.Clients;
using Grpc.Net.Client;

namespace InventoryService.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IBookServiceClient, BookServiceClient>();

        return services;
    }
}
