using Microsoft.EntityFrameworkCore;
using RealtimeAuction.Domain.Models;

namespace RealtimeAuction.Infrastructure.Context;

public interface IApplicationDbContext
{
    DbSet<Auction> Auctions { get; }
    DbSet<User> Users { get; }
    DbSet<AuctionBid> AuctionBids { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}