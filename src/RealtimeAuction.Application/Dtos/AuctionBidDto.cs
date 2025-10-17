namespace RealtimeAuction.Application.Dtos;

public record AuctionBidDto(
    Guid BidId,
    Guid AuctionId,
    Guid UserId,
    DateTime BidDate,
    decimal BidAmount);