using Microsoft.Extensions.DependencyInjection;
using RentalService.Application.UseCases.RentalRecords.Commands;
using RentalService.Application.UseCases.RentalRecords.Events;

namespace RentalService.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(AssemblyReference.Assembly));

        services.AddHttpClient<RentGeneratedDomainEventHandler>();
        services.AddHttpClient<CloseRentCommandHandler>();
        services.AddHttpClient<CancelRentCommandHandler>();

        return services;
    }
}
