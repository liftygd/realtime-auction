using RealtimeAuction.Domain.Models;
using RealtimeAuction.Domain.ValueObjects;

namespace RealtimeAuction.Application.Repositories;

public interface IReadAuctionRepository
{
    Task<List<Auction>> GetAllAuctions(CancellationToken cancellationToken = default);
    Task<Auction> GetAuctionById(AuctionId auctionId, CancellationToken cancellationToken = default);
}