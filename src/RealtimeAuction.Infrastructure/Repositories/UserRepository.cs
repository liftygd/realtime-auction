using RealtimeAuction.Application.Repositories;
using RealtimeAuction.Domain.Models;
using RealtimeAuction.Domain.ValueObjects;
using RealtimeAuction.Exceptions.Exceptions;

namespace RealtimeAuction.Infrastructure.Repositories;

public class UserRepository(ApplicationDbContext dbContext) : IUserRepository
{
    public async Task<Guid> CreateUser(User newUser, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateUser(Guid userId, User newUser, CancellationToken cancellationToken = default)
    {
        var user = await FindUser(userId, cancellationToken);
        
        throw new NotImplementedException();
    }

    public async Task<User> GetUserById(Guid userId, CancellationToken cancellationToken = default)
    {
        var user = await FindUser(userId, cancellationToken);
        
        throw new NotImplementedException();
    }

    public async Task<List<User>> GetAllUsers(CancellationToken cancellationToken = default)
    {
        return dbContext.Users.ToList();
    }

    public async Task<bool> DeleteUser(Guid userId, CancellationToken cancellationToken = default)
    {
        var user = await FindUser(userId, cancellationToken);
        
        dbContext.Users.Remove(user);
        await dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    private async Task<User> FindUser(Guid userId, CancellationToken cancellationToken = default)
    {
        var id = UserId.Create(userId);
        var user = await dbContext.Users.FindAsync([id], cancellationToken);
        if (user == null)
            throw DatabaseExceptions.EntryNonExistent<User>(nameof(id), userId.ToString());
        
        return user;
    }
}