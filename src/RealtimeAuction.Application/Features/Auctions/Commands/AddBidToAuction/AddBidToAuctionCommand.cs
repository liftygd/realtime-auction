using RealtimeAuction.Application.Abstractions;
using RealtimeAuction.Application.Dtos;

namespace RealtimeAuction.Application.Features.Auctions.Commands.AddBidToAuction;

public record AddBidToAuctionCommand(AuctionBidDto AuctionBid) : ICommand<AddBidToAuctionResult>;
public record AddBidToAuctionResult(bool IsSuccess);