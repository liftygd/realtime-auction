using RealtimeAuction.Exceptions.Exceptions;
using RealtimeAuction.Domain.Abstractions;
using RealtimeAuction.Domain.Enums;
using RealtimeAuction.Domain.ValueObjects;

namespace RealtimeAuction.Domain.Models;

public class Auction : Entity<AuctionId>
{
    public const int MIN_AUCTION_TIME_SECONDS = 3200;
    
    public UserId OwnerId { get; private set; }
    public Address Address { get; private set; }
    public AuctionStatus Status { get; private set; } = AuctionStatus.Draft;
    
    public AuctionItem AuctionItem { get; private set; }
    public BidId? HighestBid { get; private set; }
    public decimal HighestBidAmount { get; private set; }
    public decimal StartingPrice { get; private set; }
    public decimal MaxPrice { get; private set; }
    public decimal PriceIncrement { get; private set; }
    
    public DateTime? StartDate { get; private set; }
    public DateTime? LatestBidDate { get; private set; }
    public int AuctionTimeInSeconds { get; private set; }

    public IReadOnlyCollection<AuctionBid> AuctionBids => _auctionBids;
    private readonly List<AuctionBid> _auctionBids = new();

    private static void Validate(decimal startingPrice, decimal maxPrice, decimal priceIncrement, int auctionTimeInSeconds)
    {
        if (auctionTimeInSeconds < MIN_AUCTION_TIME_SECONDS)
            throw ErrorExceptions.ValueOutsideBounds<Auction>(nameof(auctionTimeInSeconds), MIN_AUCTION_TIME_SECONDS, Int32.MaxValue);
        
        if (maxPrice < 0)
            throw ErrorExceptions.Negative<Auction>(nameof(maxPrice));
        
        if (startingPrice < 0)
            throw ErrorExceptions.Negative<Auction>(nameof(startingPrice));
        
        if (priceIncrement < 0)
            throw ErrorExceptions.Negative<Auction>(nameof(priceIncrement));
        
        if (maxPrice - startingPrice < priceIncrement)
            throw ErrorExceptions.ValueOutsideBounds<Auction>(nameof(maxPrice), (int)(startingPrice + priceIncrement), Int32.MaxValue);
    }

    public static Auction Create(
        AuctionId auctionId, 
        UserId ownerId,
        Address address,
        AuctionItem auctionItem, 
        decimal startingPrice, 
        decimal maxPrice, 
        decimal priceIncrement, 
        int auctionTimeInSeconds)
    {
        Validate(startingPrice, maxPrice, priceIncrement, auctionTimeInSeconds);
        
        var auction = new Auction
        {
            Id = auctionId,
            OwnerId = ownerId,
            Address = address,
            Status = AuctionStatus.Draft,
            AuctionItem = auctionItem,
            HighestBid = null,
            HighestBidAmount = startingPrice,
            StartingPrice = startingPrice,
            MaxPrice = maxPrice,
            PriceIncrement = priceIncrement,
            StartDate = null,
            LatestBidDate = null,
            AuctionTimeInSeconds = auctionTimeInSeconds
        };

        return auction;
    }

    public void Update(
        Address newAddress, 
        AuctionStatus newStatus, 
        AuctionItem newAuctionItem,
        decimal newStartingPrice,
        decimal newMaxPrice,
        decimal newPriceIncrement,
        int newAuctionTimeInSeconds)
    {
        if (Status != AuctionStatus.Draft)
            throw AuctionExceptions.AuctionAlreadyActive<Auction>(Id.Value.ToString());

        Validate(newStartingPrice, newMaxPrice, newPriceIncrement, newAuctionTimeInSeconds);
        
        if (Status == AuctionStatus.Draft && newStatus == AuctionStatus.Active)
        {
            StartDate = DateTime.UtcNow;
            LatestBidDate = DateTime.UtcNow;
        }
        
        Address = newAddress;
        Status = newStatus;
        AuctionItem = newAuctionItem;
        StartingPrice = newStartingPrice;
        MaxPrice = newMaxPrice;
        PriceIncrement = newPriceIncrement;
        AuctionTimeInSeconds = newAuctionTimeInSeconds;
    }

    public AuctionBid Add(UserId userId, DateTime bidDate, decimal price)
    {
        if (!IsActive())
            throw AuctionExceptions.AuctionIsNotActive<Auction>(Id.Value.ToString());
        
        var bid = AuctionBid.Create(Id, userId, bidDate, price);
        
        if (price < 0)
            throw ErrorExceptions.Negative<Auction>(nameof(price));

        if (price < HighestBidAmount)
            throw AuctionExceptions.BidTooSmall<Auction>(bid.Id.Value.ToString(), HighestBidAmount);
        
        if (price - HighestBidAmount == 0 || (price - HighestBidAmount) % PriceIncrement != 0) 
            throw AuctionExceptions.BidIsNotIncrementedCorrectly<Auction>(bid.Id.Value.ToString(), PriceIncrement);
        
        _auctionBids.Add(bid);
        
        HighestBid = bid.Id;
        HighestBidAmount = bid.Price;
        LatestBidDate = bidDate;

        return bid;
    }

    public bool IsActive()
    {
        if (Status != AuctionStatus.Active)
            return false;

        if (LatestBidDate == null)
            return false;
        
        var currTime = DateTime.UtcNow;
        var timeElapsed = currTime.Subtract(LatestBidDate.Value);

        return timeElapsed.TotalSeconds < AuctionTimeInSeconds;
    }

    public void Close()
    {
        Status = AuctionStatus.Ended;
    }
}