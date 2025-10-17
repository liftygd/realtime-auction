using RealtimeAuction.Application.Abstractions;
using RealtimeAuction.Application.Dtos;

namespace RealtimeAuction.Application.Features.Auctions.Commands.CreateAuction;

public record CreateAuctionCommand(AuctionDto Auction) : ICommand<CreateAuctionResult>;
public record CreateAuctionResult(Guid Id);