using RealtimeAuction.Application.Abstractions;
using RealtimeAuction.Application.Extensions;
using RealtimeAuction.Application.Repositories;
using RealtimeAuction.Domain.ValueObjects;

namespace RealtimeAuction.Application.Features.Auctions.Queries.GetAuctionById;

public class GetAuctionByIdQueryHandler(IReadAuctionRepository readAuctionRepository) : IQueryHandler<GetAuctionByIdQuery, GetAuctionByIdResult>
{
    public async Task<GetAuctionByIdResult> Handle(GetAuctionByIdQuery query, CancellationToken cancellationToken = default)
    {
        var auction = await readAuctionRepository.GetAuctionById(AuctionId.Create(query.Id), cancellationToken);
        return new GetAuctionByIdResult(auction.ToAuctionDto());
    }
}