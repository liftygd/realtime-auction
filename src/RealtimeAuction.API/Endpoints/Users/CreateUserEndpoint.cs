using Auction.API.Abstractions;
using Mapster;
using RealtimeAuction.Application.Abstractions;
using RealtimeAuction.Application.Users.Commands.CreateUser;

namespace Auction.API.Endpoints.Users;

public record CreateUserRequest(string Username, string EmailAddress, DateTime Birthday);

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
            return Results.Created($"users/{command.Username}", response);
        });
    }
}