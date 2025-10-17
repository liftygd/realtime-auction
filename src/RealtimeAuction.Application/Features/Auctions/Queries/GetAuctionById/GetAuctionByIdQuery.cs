using RealtimeAuction.Application.Abstractions;
using RealtimeAuction.Application.Dtos;

namespace RealtimeAuction.Application.Features.Auctions.Queries.GetAuctionById;

public record GetAuctionByIdQuery(Guid Id) : IQuery<GetAuctionByIdResult>;
public record GetAuctionByIdResult(AuctionDto Auction);