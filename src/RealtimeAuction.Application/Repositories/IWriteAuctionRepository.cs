using RealtimeAuction.Domain.Models;
using RealtimeAuction.Domain.ValueObjects;

namespace RealtimeAuction.Application.Repositories;

public interface IWriteAuctionRepository
{
    Task<Guid> CreateAuction(Auction newAuction, CancellationToken cancellationToken = default);
    Task<bool> AddBid(AuctionId auctionId, AuctionBid newBid, CancellationToken cancellationToken = default);
    Task<bool> UpdateAuction(AuctionId auctionId, Auction newAuction, CancellationToken cancellationToken = default);
    Task<bool> DeleteAuction(AuctionId auctionId, CancellationToken cancellationToken = default);
}