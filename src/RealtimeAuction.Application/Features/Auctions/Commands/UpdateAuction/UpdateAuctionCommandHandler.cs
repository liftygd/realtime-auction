using RealtimeAuction.Application.Abstractions;
using RealtimeAuction.Application.Extensions;
using RealtimeAuction.Application.Repositories;
using RealtimeAuction.Domain.ValueObjects;

namespace RealtimeAuction.Application.Features.Auctions.Commands.UpdateAuction;

public class UpdateAuctionCommandHandler(IWriteAuctionRepository writeAuctionRepository) : ICommandHandler<UpdateAuctionCommand, UpdateAuctionResult>
{
    public async Task<UpdateAuctionResult> Handle(UpdateAuctionCommand command, CancellationToken cancellationToken = default)
    {
        var result = await writeAuctionRepository.UpdateAuction(
            AuctionId.Create(command.Auction.AuctionId), 
            command.Auction.ToAuction(command.Auction.AuctionId),
            cancellationToken);
        
        return new UpdateAuctionResult(result);
    }
}