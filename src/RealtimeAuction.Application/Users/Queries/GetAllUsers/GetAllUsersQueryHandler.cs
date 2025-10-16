using RealtimeAuction.Application.Abstractions;
using RealtimeAuction.Application.Extensions;
using RealtimeAuction.Application.Repositories;

namespace RealtimeAuction.Application.Users.Queries.GetAllUsers;

public class GetAllUsersQueryHandler(IReadUserRepository readUserRepository) : IQueryHandler<GetAllUsersQuery, GetAllUsersResult>
{
    public async Task<GetAllUsersResult> Handle(GetAllUsersQuery query, CancellationToken cancellationToken = default)
    {
        var users = await readUserRepository.GetAllUsers(cancellationToken);
        return new GetAllUsersResult(users.ToUserDtoList());
    }
}