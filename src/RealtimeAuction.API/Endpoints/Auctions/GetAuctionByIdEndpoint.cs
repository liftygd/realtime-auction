using Auction.API.Abstractions;
using Mapster;
using RealtimeAuction.Application.Abstractions;
using RealtimeAuction.Application.Dtos;
using RealtimeAuction.Application.Features.Auctions.Queries.GetAuctionById;

namespace Auction.API.Endpoints.Auctions;

//public record GetAuctionByIdRequest(Guid Id);
public record GetAuctionByIdResponse(AuctionDto Auction);

public class GetAuctionByIdEndpoint : IMinimalEndpoint
{
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("/auctions/{id}", async (Guid id, IMediator mediator) =>
        {
            var result = await mediator.Send<GetAuctionByIdQuery, GetAuctionByIdResult>(new GetAuctionByIdQuery(id));
            
            var response = result.Adapt<GetAuctionByIdResponse>();
            return Results.Ok(response);
        });
    }
}