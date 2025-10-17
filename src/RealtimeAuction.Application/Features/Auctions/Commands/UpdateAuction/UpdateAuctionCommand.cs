using RealtimeAuction.Application.Abstractions;
using RealtimeAuction.Application.Dtos;

namespace RealtimeAuction.Application.Features.Auctions.Commands.UpdateAuction;

public record UpdateAuctionCommand(AuctionDto Auction) : ICommand<UpdateAuctionResult>;
public record UpdateAuctionResult(bool IsSuccess);