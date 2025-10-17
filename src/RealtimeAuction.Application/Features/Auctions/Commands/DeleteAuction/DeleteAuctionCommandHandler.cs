using RealtimeAuction.Application.Abstractions;
using RealtimeAuction.Application.Repositories;
using RealtimeAuction.Domain.ValueObjects;

namespace RealtimeAuction.Application.Features.Auctions.Commands.DeleteAuction;

public class DeleteAuctionCommandHandler(IWriteAuctionRepository writeAuctionRepository) : ICommandHandler<DeleteAuctionCommand, DeleteAuctionResult>
{
    public async Task<DeleteAuctionResult> Handle(DeleteAuctionCommand command, CancellationToken cancellationToken = default)
    {
        var result = await writeAuctionRepository.DeleteAuction(AuctionId.Create(command.Id), cancellationToken);
        return new DeleteAuctionResult(result);
    }
}