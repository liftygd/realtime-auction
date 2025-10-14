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

    private AuctionBid(
        AuctionId auctionId, 
        UserId userId, 
        DateTime biddingDate, 
        decimal price)
    {
        AuctionId = auctionId;
        UserId = userId;
        BiddingDate = biddingDate;
        Price = price;
    }

    public static AuctionBid Create(Guid auctionId, Guid userId, DateTime? bidDate, decimal price)
    {
        var auction = AuctionId.Create(auctionId);
        var user = UserId.Create(userId);

        if (price <= 0)
            throw ErrorExceptions.ZeroOrNegative<AuctionBid>(nameof(price));

        if (bidDate == null)
            bidDate = DateTime.UtcNow;

        return new AuctionBid(auction, user, bidDate.Value, price);
    }
}
