using RealtimeAuction.Application.Hubs;

namespace Auction.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddSignalR();

        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        app.MapHub<AuctionHub>("/auction/hub");

        return app;
    }
}
