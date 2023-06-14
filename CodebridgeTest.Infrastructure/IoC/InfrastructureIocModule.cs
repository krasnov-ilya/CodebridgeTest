using CodebridgeTest.Infrastructure.Interfaces;
using CodebridgeTest.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CodebridgeTest.Infrastructure.IoC;

public static class InfrastructureIocModule
{
    public static IServiceCollection RegisterInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<IDogsService, DogsService>();
        
        return services;
    }
}