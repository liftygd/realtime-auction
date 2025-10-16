using Auction.API.Abstractions;
using Mapster;

namespace Auction.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddMapster();
        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        var endpoints = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => typeof(IMinimalEndpoint).IsAssignableFrom(p) && p.IsClass && !p.IsAbstract)
            .Select(Activator.CreateInstance)
            .Cast<IMinimalEndpoint>();
        
        foreach (var endpoint in endpoints)
            endpoint.AddRoute(app);
        
        return app;
    }
}
