using BookService.Domain.Repositories;
using BookService.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BookService.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
