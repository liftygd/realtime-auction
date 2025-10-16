using RealtimeAuction.Application.Abstractions;
using RealtimeAuction.Application.Dtos;

namespace RealtimeAuction.Application.Features.Users.Queries.GetAllUsers;

public record GetAllUsersQuery() : IQuery<GetAllUsersResult>;
public record GetAllUsersResult(List<UserDto> Users);