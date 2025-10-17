using Auction.API.Abstractions;
using Mapster;
using RealtimeAuction.Application.Abstractions;
using RealtimeAuction.Application.Dtos;
using RealtimeAuction.Application.Features.Users.Commands.UpdateUser;

namespace Auction.API.Endpoints.Users;

public record UpdateUserRequest(UserDto User);
public record UpdateUserResponse(bool IsSuccess);

public class UpdateUserEndpoint : IMinimalEndpoint
{
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPut("/users/{id}", async (Guid id, UpdateUserRequest request, IMediator mediator) =>
        {
            var updateCommand = new UpdateUserCommand(id, request.User);
            var result = await mediator.Send<UpdateUserCommand, UpdateUserResult>(updateCommand);

            var response = result.Adapt<UpdateUserResponse>();
            return Results.Ok(response);
        });
    }
}