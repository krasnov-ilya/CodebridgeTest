using CodebridgeTest.Domain.Interfaces;
using CodebridgeTest.Persistence.Context;
using CodebridgeTest.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CodebridgeTest.Persistence.IoC;

public static class PersistenceIocModule
{
    public static IServiceCollection RegisterPersistence(this IServiceCollection services)
    {
        services.AddDbContext<DogsContext>();

        services.AddTransient<IDogsRepository, DogsRepository>();
        
        return services;
    }
}