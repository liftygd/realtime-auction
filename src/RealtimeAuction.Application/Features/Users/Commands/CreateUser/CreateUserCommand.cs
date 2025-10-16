using RealtimeAuction.Application.Abstractions;
using RealtimeAuction.Application.Dtos;

namespace RealtimeAuction.Application.Features.Users.Commands.CreateUser;

public record CreateUserCommand(UserDto User) : ICommand<CreateUserResult>;
public record CreateUserResult(Guid Id);