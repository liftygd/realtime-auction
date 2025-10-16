using RealtimeAuction.Application.Abstractions;
using RealtimeAuction.Application.Dtos;
using RealtimeAuction.Domain.Models;

namespace RealtimeAuction.Application.Users.Queries.GetAllUsers;

public record GetAllUsersQuery() : IQuery<GetAllUsersResult>;

public record GetAllUsersResult(List<UserDto> Users);