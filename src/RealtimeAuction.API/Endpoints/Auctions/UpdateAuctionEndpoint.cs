using Auction.API.Abstractions;
using Mapster;
using RealtimeAuction.Application.Abstractions;
using RealtimeAuction.Application.Dtos;
using RealtimeAuction.Application.Features.Auctions.Commands.UpdateAuction;

namespace Auction.API.Endpoints.Auctions;

public record UpdateAuctionRequest(AuctionDto Auction);
public record UpdateAuctionResponse(bool IsSuccess);

public class UpdateAuctionEndpoint : IMinimalEndpoint
{
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPut("/auctions", async (UpdateAuctionRequest request, IMediator mediator) =>
        {
            var command = request.Adapt<UpdateAuctionCommand>();
            var result = await mediator.Send<UpdateAuctionCommand, UpdateAuctionResult>(command);
            
            var response = result.Adapt<UpdateAuctionResponse>();
            return Results.Ok(response);
        });
    }
}