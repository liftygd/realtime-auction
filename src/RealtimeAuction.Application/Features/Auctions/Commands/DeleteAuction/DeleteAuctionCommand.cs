using RealtimeAuction.Application.Abstractions;

namespace RealtimeAuction.Application.Features.Auctions.Commands.DeleteAuction;

public record DeleteAuctionCommand(Guid Id) : ICommand<DeleteAuctionResult>;
public record DeleteAuctionResult(bool IsSuccess);