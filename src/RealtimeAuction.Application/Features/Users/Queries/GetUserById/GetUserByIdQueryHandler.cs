using RealtimeAuction.Application.Abstractions;
using RealtimeAuction.Application.Extensions;
using RealtimeAuction.Application.Repositories;
using RealtimeAuction.Domain.ValueObjects;

namespace RealtimeAuction.Application.Features.Users.Queries.GetUserById;

public class GetUserByIdQueryHandler(IReadUserRepository readUserRepository) : IQueryHandler<GetUserByIdQuery, GetUserByIdResult>
{
    public async Task<GetUserByIdResult> Handle(GetUserByIdQuery query, CancellationToken cancellationToken = default)
    {
        var user = await readUserRepository.GetUserById(UserId.Create(query.Id), cancellationToken);
        return new GetUserByIdResult(user.ToUserDto());
    }
}