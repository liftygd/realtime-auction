using Auction.API.Abstractions;
using Mapster;
using RealtimeAuction.Application.Abstractions;
using RealtimeAuction.Application.Features.Users.Commands.DeleteUser;

namespace Auction.API.Endpoints.Users;

//public record DeleteUserRequest(Guid Id);
public record DeleteUserResponse(bool IsSuccess);

public class DeleteUserEndpoint : IMinimalEndpoint
{
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapDelete("/users/{id}", async (Guid id, IMediator mediator) =>
        {
            var result = await mediator.Send<DeleteUserCommand, DeleteUserResult>(new DeleteUserCommand(id));
            
            var response = result.Adapt<DeleteUserResponse>();
            return Results.Ok(response);
        });
    }
}