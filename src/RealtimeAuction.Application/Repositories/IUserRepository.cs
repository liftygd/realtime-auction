using RealtimeAuction.Domain.Models;

namespace RealtimeAuction.Application.Repositories;

public interface IUserRepository
{
    Task<Guid> CreateUser(User newUser, CancellationToken cancellationToken = default);
    Task<bool> UpdateUser(Guid userId, User newUser, CancellationToken cancellationToken = default);
    Task<User> GetUserById(Guid userId, CancellationToken cancellationToken = default);
    Task<List<User>> GetAllUsers(CancellationToken cancellationToken = default);
    Task<bool> DeleteUser(Guid userId, CancellationToken cancellationToken = default);
}