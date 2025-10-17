using Auction.API.Abstractions;
using Mapster;
using RealtimeAuction.Application.Abstractions;
using RealtimeAuction.Application.Features.Auctions.Commands.DeleteAuction;

namespace Auction.API.Endpoints.Auctions;

//public record DeleteAuctionRequest(Guid Id);
public record DeleteAuctionResponse(bool IsSuccess);

public class DeleteAuctionEndpoint : IMinimalEndpoint
{
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapDelete("/auctions/{id}", async (Guid id, IMediator mediator) =>
        {
            var result = await mediator.Send<DeleteAuctionCommand, DeleteAuctionResult>(new DeleteAuctionCommand(id));
            
            var response = result.Adapt<DeleteAuctionResponse>();
            return Results.Ok(response);
        });
    }
}