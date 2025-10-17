using RealtimeAuction.Domain.Enums;

namespace RealtimeAuction.Application.Dtos;

public record AuctionDto(
    Guid AuctionId,
    Guid OwnerId,
    AddressDto Address,
    AuctionStatus Status,
    AuctionItemDto AuctionItem,
    Guid? HighestBid,
    decimal HighestBidAmount,
    decimal StartingPrice,
    decimal MaxPrice,
    decimal PriceIncrement,
    DateTime? StartDate,
    DateTime? LatestBidDate,
    int AuctionTimeInSeconds,
    List<AuctionBidDto> AuctionBids);