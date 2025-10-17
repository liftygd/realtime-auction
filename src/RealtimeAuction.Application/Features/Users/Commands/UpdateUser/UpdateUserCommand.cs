using RealtimeAuction.Application.Abstractions;
using RealtimeAuction.Application.Dtos;

namespace RealtimeAuction.Application.Features.Users.Commands.UpdateUser;

public record UpdateUserCommand(Guid Id, UserDto User) : ICommand<UpdateUserResult>;
public record UpdateUserResult(bool IsSuccess);