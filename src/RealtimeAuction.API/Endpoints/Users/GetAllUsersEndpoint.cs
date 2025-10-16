using Auction.API.Abstractions;
using Mapster;
using RealtimeAuction.Application.Abstractions;
using RealtimeAuction.Application.Dtos;
using RealtimeAuction.Application.Features.Users.Queries.GetAllUsers;

namespace Auction.API.Endpoints.Users;

//public record CreateUserRequest(string Username, string EmailAddress, DateTime Birthday);

public record GetAllUsersResponse(List<UserDto> Users);

public class GetAllUsersEndpoint : IMinimalEndpoint
{
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("/users", async (IMediator mediator) =>
        {
            var result = await mediator.Send<GetAllUsersQuery, GetAllUsersResult>(new GetAllUsersQuery());
            
            var response = result.Adapt<GetAllUsersResponse>();
            return Results.Ok(response);
        });
    }
}