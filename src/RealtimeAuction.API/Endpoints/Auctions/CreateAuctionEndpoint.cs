using Auction.API.Abstractions;
using Mapster;
using RealtimeAuction.Application.Abstractions;
using RealtimeAuction.Application.Dtos;
using RealtimeAuction.Application.Features.Auctions.Commands.CreateAuction;

namespace Auction.API.Endpoints.Auctions;

public record CreateAuctionRequest(AuctionDto Auction);
public record CreateAuctionResponse(Guid Id);

public class CreateAuctionEndpoint : IMinimalEndpoint
{
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("/auctions", async (CreateAuctionRequest request, IMediator mediator) =>
        {
            var command = request.Adapt<CreateAuctionCommand>();
            var result = await mediator.Send<CreateAuctionCommand, CreateAuctionResult>(command);
            
            var response = result.Adapt<CreateAuctionResponse>();
            return Results.Created($"auctions/{command.Auction.AuctionId}", response);
        });
    }
}