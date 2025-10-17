using Auction.API.Abstractions;
using Mapster;
using RealtimeAuction.Application.Abstractions;
using RealtimeAuction.Application.Dtos;
using RealtimeAuction.Application.Features.Auctions.Commands.AddBidToAuction;

namespace Auction.API.Endpoints.Auctions;

public record AddBidToAuctionRequest(AuctionBidDto AuctionBid);
public record AddBidToAuctionResponse(bool IsSuccess);

public class AddBidToAuctionEndpoint : IMinimalEndpoint
{
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("/auctions/bids", async (AddBidToAuctionRequest request, IMediator mediator) =>
        {
            var command = request.Adapt<AddBidToAuctionCommand>();
            var result = await mediator.Send<AddBidToAuctionCommand, AddBidToAuctionResult>(command);
            
            var response = result.Adapt<AddBidToAuctionResponse>();
            return Results.Ok(response);
        });
    }
}