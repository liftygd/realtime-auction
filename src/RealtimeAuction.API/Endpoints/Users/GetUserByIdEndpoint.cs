using Auction.API.Abstractions;
using Mapster;
using RealtimeAuction.Application.Abstractions;
using RealtimeAuction.Application.Dtos;
using RealtimeAuction.Application.Features.Users.Queries.GetUserById;

namespace Auction.API.Endpoints.Users;

//public record GetUserByIdRequest(Guid Id);
public record GetUserByIdResponse(UserDto User);

public class GetUserByIdEndpoint : IMinimalEndpoint
{
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("/users/{id}", async (Guid id, IMediator mediator) =>
        {
            var result = await mediator.Send<GetUserByIdQuery, GetUserByIdResult>(new GetUserByIdQuery(id));
            
            var response = result.Adapt<GetUserByIdResponse>();
            return Results.Ok(response);
        });
    }
}