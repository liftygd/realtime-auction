using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using RealtimeAuction.Application.Hubs;
using RealtimeAuction.Application.Abstractions;
using RealtimeAuction.Application.Utility;

namespace RealtimeAuction.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddSignalR();
        services.AddScoped<IMediator, Mediator>();

        services.Scan(scan => scan
            .FromAssemblies(Assembly.GetExecutingAssembly())
            .AddClasses(cls => cls.AssignableTo(typeof(IRequestHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }

    public static WebApplication UseApplicationServices(this WebApplication app)
    {
        app.MapHub<AuctionHub>("/auction/hub");

        return app;
    }
}
