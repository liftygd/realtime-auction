namespace RealtimeAuction.Application.Dtos;

public record AuctionItemDto(
    string ItemName,
    string ItemDescription,
    int Amount,
    decimal Price);