using RealtimeAuction.Application.Abstractions;
using RealtimeAuction.Application.Repositories;
using RealtimeAuction.Domain.Models;
using RealtimeAuction.Domain.ValueObjects;

namespace RealtimeAuction.Application.Users.Commands.CreateUser;

public class CreateUserCommandHandler(IWriteUserRepository writeUserRepository) : ICommandHandler<CreateUserCommand, CreateUserResult>
{
    public async Task<CreateUserResult> Handle(CreateUserCommand command, CancellationToken cancellationToken = default)
    {
        var user = User.Create(UserId.Create(Guid.NewGuid()), command.Username, command.EmailAddress, command.Birthday);
        var result = await writeUserRepository.CreateUser(user, cancellationToken);

        return new CreateUserResult(result);
    }
}