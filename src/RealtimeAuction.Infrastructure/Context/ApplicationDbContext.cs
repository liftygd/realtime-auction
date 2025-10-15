using System.Reflection;
using Microsoft.EntityFrameworkCore;
using RealtimeAuction.Domain.Models;

namespace RealtimeAuction.Infrastructure.Context;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<Auction> Auctions => Set<Auction>();
    public DbSet<User> Users => Set<User>();
    public DbSet<AuctionBid> AuctionBids => Set<AuctionBid>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}