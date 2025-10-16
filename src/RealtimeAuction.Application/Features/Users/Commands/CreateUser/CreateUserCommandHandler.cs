using RealtimeAuction.Application.Abstractions;
using RealtimeAuction.Application.Extensions;
using RealtimeAuction.Application.Repositories;

namespace RealtimeAuction.Application.Features.Users.Commands.CreateUser;

public class CreateUserCommandHandler(IWriteUserRepository writeUserRepository) : ICommandHandler<CreateUserCommand, CreateUserResult>
{
    public async Task<CreateUserResult> Handle(CreateUserCommand command, CancellationToken cancellationToken = default)
    {
        var result = await writeUserRepository.CreateUser(command.User.ToUser(Guid.NewGuid()), cancellationToken);
        return new CreateUserResult(result);
    }
}