using Auction.API.Abstractions;
using Mapster;
using RealtimeAuction.Application.Abstractions;
using RealtimeAuction.Application.Dtos;
using RealtimeAuction.Application.Features.Users.Commands.CreateUser;

namespace Auction.API.Endpoints.Users;

public record CreateUserRequest(UserDto User);
public record CreateUserResponse(Guid Id);

public class CreateUserEndpoint : IMinimalEndpoint
{
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPost("/users", async (CreateUserRequest request, IMediator mediator) =>
        {
            var command = request.Adapt<CreateUserCommand>();
            var result = await mediator.Send<CreateUserCommand, CreateUserResult>(command);
            
            var response = result.Adapt<CreateUserResponse>();
            return Results.Created($"users/{command.User.Username}", response);
        });
    }
}