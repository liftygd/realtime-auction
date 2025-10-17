using RealtimeAuction.Application.Abstractions;
using RealtimeAuction.Application.Extensions;
using RealtimeAuction.Application.Repositories;

namespace RealtimeAuction.Application.Features.Auctions.Queries.GetAllAuctions;

public class GetAllAuctionsQueryHandler(IReadAuctionRepository readAuctionRepository) : IQueryHandler<GetAllAuctionsQuery, GetAllAuctionsResult>
{
    public async Task<GetAllAuctionsResult> Handle(GetAllAuctionsQuery query, CancellationToken cancellationToken = default)
    {
        var auctions = await readAuctionRepository.GetAllAuctions(cancellationToken);
        return new GetAllAuctionsResult(auctions.ToAuctionDtoList());
    }
}