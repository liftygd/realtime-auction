using Auction.Domain.Abstractions;
using Auction.Domain.ValueObjects;
using Auction.Exceptions.Exceptions;

namespace Auction.Domain.Models;

public class AuctionBid : Entity<BidId>
{
    public AuctionId AuctionId { get; private set; }
    public UserId UserId { get; private set; }
    public DateTime BiddingDate { get; private set; } = DateTime.UtcNow;
    public decimal Price { get; private set; } = 0;

    public static AuctionBid Create(AuctionId auctionId, UserId userId, DateTime bidDate, decimal price)
    {
        if (price <= 0)
            throw ErrorExceptions.ZeroOrNegative<AuctionBid>(nameof(price));

        var bid = new AuctionBid
        {
            AuctionId = auctionId,
            UserId = userId,
            BiddingDate = bidDate,
            Price = price
        };

        return bid;
    }
}
