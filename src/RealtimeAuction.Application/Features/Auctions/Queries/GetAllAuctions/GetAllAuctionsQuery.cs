using RealtimeAuction.Application.Abstractions;
using RealtimeAuction.Application.Dtos;

namespace RealtimeAuction.Application.Features.Auctions.Queries.GetAllAuctions;

public record GetAllAuctionsQuery() : IQuery<GetAllAuctionsResult>;
public record GetAllAuctionsResult(List<AuctionDto> Auctions);