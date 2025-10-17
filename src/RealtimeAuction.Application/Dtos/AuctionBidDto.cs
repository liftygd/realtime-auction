namespace RealtimeAuction.Application.Dtos;

public record AuctionBidDto(
    Guid BidId,
    Guid AuctionId,
    DateTime BidDate,
    decimal BidAmount);