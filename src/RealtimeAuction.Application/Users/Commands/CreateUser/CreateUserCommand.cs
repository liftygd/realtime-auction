using RealtimeAuction.Application.Abstractions;

namespace RealtimeAuction.Application.Users.Commands.CreateUser;

public record CreateUserCommand(string Username, string EmailAddress, DateTime Birthday)
    : ICommand<CreateUserResult>;

public record CreateUserResult(Guid Id);