using RealtimeAuction.Application.Abstractions;
using RealtimeAuction.Application.Dtos;

namespace RealtimeAuction.Application.Features.Users.Queries.GetUserById;

public record GetUserByIdQuery(Guid Id) : IQuery<GetUserByIdResult>;
public record GetUserByIdResult(UserDto User);