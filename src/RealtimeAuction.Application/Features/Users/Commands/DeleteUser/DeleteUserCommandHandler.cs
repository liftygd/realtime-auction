using RealtimeAuction.Application.Abstractions;
using RealtimeAuction.Application.Repositories;
using RealtimeAuction.Domain.ValueObjects;

namespace RealtimeAuction.Application.Features.Users.Commands.DeleteUser;

public class DeleteUserCommandHandler(IWriteUserRepository writeUserRepository) : ICommandHandler<DeleteUserCommand, DeleteUserResult>
{
    public async Task<DeleteUserResult> Handle(DeleteUserCommand command, CancellationToken cancellationToken = default)
    {
        var result = await writeUserRepository.DeleteUser(UserId.Create(command.Id), cancellationToken);
        return new DeleteUserResult(result);
    }
}