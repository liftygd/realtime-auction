using RealtimeAuction.Application.Abstractions;
using RealtimeAuction.Application.Extensions;
using RealtimeAuction.Application.Repositories;
using RealtimeAuction.Domain.ValueObjects;

namespace RealtimeAuction.Application.Features.Users.Commands.UpdateUser;

public class UpdateUserCommandHandler(IWriteUserRepository writeUserRepository) : ICommandHandler<UpdateUserCommand, UpdateUserResult>
{
    public async Task<UpdateUserResult> Handle(UpdateUserCommand command, CancellationToken cancellationToken = default)
    {
        var result = await writeUserRepository.UpdateUser(UserId.Create(command.Id), command.User.ToUser(command.Id), cancellationToken);
        return new UpdateUserResult(result);
    }
}