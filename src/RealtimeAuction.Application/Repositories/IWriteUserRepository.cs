using RealtimeAuction.Domain.Models;
using RealtimeAuction.Domain.ValueObjects;

namespace RealtimeAuction.Application.Repositories;

public interface IWriteUserRepository
{
    Task<Guid> CreateUser(User newUser, CancellationToken cancellationToken = default);
    Task<bool> UpdateUser(UserId userId, User newUser, CancellationToken cancellationToken = default);
    Task<bool> DeleteUser(UserId userId, CancellationToken cancellationToken = default);
}