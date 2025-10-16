using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealtimeAuction.Application.Repositories;
using RealtimeAuction.Infrastructure.Context;
using RealtimeAuction.Infrastructure.Repositories;

namespace RealtimeAuction.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IWriteUserRepository, UserRepository>();
        services.AddScoped<IReadUserRepository, UserRepository>();
        
        services.AddScoped<IWriteAuctionRepository, AuctionRepository>();
        services.AddScoped<IReadAuctionRepository, AuctionRepository>();
        
        var connectionString = configuration.GetConnectionString("Database");
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });
        
        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        
        return services;
    }
}