using RealtimeAuction.Application.Abstractions;

namespace RealtimeAuction.Application.Features.Users.Commands.DeleteUser;

public record DeleteUserCommand(Guid Id) : ICommand<DeleteUserResult>;
public record DeleteUserResult(bool IsSuccess);