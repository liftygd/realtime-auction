using Auction.API.Abstractions;
using Mapster;
using RealtimeAuction.Application.Abstractions;
using RealtimeAuction.Application.Dtos;
using RealtimeAuction.Application.Features.Auctions.Queries.GetAllAuctions;

namespace Auction.API.Endpoints.Auctions;

//public record GetAllAuctionsRequest();
public record GetAllAuctionsResponse(List<AuctionDto> Auctions);

public class GetAllAuctionsEndpoint : IMinimalEndpoint
{
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("/auctions", async (IMediator mediator) =>
        {
            var result = await mediator.Send<GetAllAuctionsQuery, GetAllAuctionsResult>(new GetAllAuctionsQuery());
            
            var response = result.Adapt<GetAllAuctionsResponse>();
            return Results.Ok(response);
        });
    }
}