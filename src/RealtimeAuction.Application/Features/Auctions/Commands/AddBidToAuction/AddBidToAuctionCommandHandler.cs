using RealtimeAuction.Application.Abstractions;
using RealtimeAuction.Application.Repositories;
using RealtimeAuction.Domain.Models;
using RealtimeAuction.Domain.ValueObjects;

namespace RealtimeAuction.Application.Features.Auctions.Commands.AddBidToAuction;

public class AddBidToAuctionCommandHandler(IWriteAuctionRepository writeAuctionRepository) : ICommandHandler<AddBidToAuctionCommand, AddBidToAuctionResult>
{
    public async Task<AddBidToAuctionResult> Handle(AddBidToAuctionCommand command, CancellationToken cancellationToken = default)
    {
        var bid = AuctionBid.Create(
            AuctionId.Create(command.AuctionBid.AuctionId), 
            UserId.Create(command.AuctionBid.UserId),
            DateTime.UtcNow, 
            command.AuctionBid.BidAmount);

        var result = await writeAuctionRepository.AddBid(bid.AuctionId, bid, cancellationToken);
        return new AddBidToAuctionResult(result);
    }
}