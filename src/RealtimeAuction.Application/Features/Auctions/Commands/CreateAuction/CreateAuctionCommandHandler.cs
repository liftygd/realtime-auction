using RealtimeAuction.Application.Abstractions;
using RealtimeAuction.Application.Extensions;
using RealtimeAuction.Application.Repositories;
using RealtimeAuction.Domain.Models;
using RealtimeAuction.Domain.ValueObjects;

namespace RealtimeAuction.Application.Features.Auctions.Commands.CreateAuction;

public class CreateAuctionCommandHandler(IWriteAuctionRepository writeAuctionRepository) : ICommandHandler<CreateAuctionCommand, CreateAuctionResult>
{
    public async Task<CreateAuctionResult> Handle(CreateAuctionCommand command, CancellationToken cancellationToken = default)
    {
        var result = await writeAuctionRepository.CreateAuction(command.Auction.ToAuction(Guid.NewGuid()), cancellationToken);
        return new CreateAuctionResult(result);
    }
}