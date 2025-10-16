using Microsoft.EntityFrameworkCore;
using Npgsql;
using RealtimeAuction.Application.Repositories;
using RealtimeAuction.Domain.Models;
using RealtimeAuction.Domain.ValueObjects;
using RealtimeAuction.Exceptions.Exceptions;
using RealtimeAuction.Infrastructure.Context;

namespace RealtimeAuction.Infrastructure.Repositories;

public class UserRepository(IApplicationDbContext dbContext) : IUserRepository
{
    public async Task<Guid> CreateUser(User newUser, CancellationToken cancellationToken = default)
    {
        try
        {
            dbContext.Users.Add(newUser);
            await dbContext.SaveChangesAsync(cancellationToken);
            
            return newUser.Id.Value;
        }
        catch (DbUpdateException ex)
        {
            if (ex.InnerException is PostgresException npgex && npgex.SqlState == PostgresErrorCodes.UniqueViolation)
                throw DatabaseExceptions.EntryValueNotUnique<User>($"{nameof(newUser.Username)} or {nameof(newUser.EmailAddress)}");

            throw;
        }
    }

    public async Task<bool> UpdateUser(Guid userId, User newUser, CancellationToken cancellationToken = default)
    {
        var user = await FindUser(userId, cancellationToken);
        
        try
        {
            user.Update(newUser.Username, newUser.EmailAddress, newUser.Birthday.ToDateTime());
            
            dbContext.Users.Update(user);
            await dbContext.SaveChangesAsync(cancellationToken);
            
            return true;
        }
        catch (DbUpdateException ex)
        {
            if (ex.InnerException is PostgresException npgex && npgex.SqlState == PostgresErrorCodes.UniqueViolation)
                throw DatabaseExceptions.EntryValueNotUnique<User>($"{nameof(newUser.Username)} or {nameof(newUser.EmailAddress)}");

            throw;
        }
    }

    public async Task<User> GetUserById(Guid userId, CancellationToken cancellationToken = default)
    {
        var user = await FindUser(userId, cancellationToken);
        return user;
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
        var user = await dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
        
        if (user == null)
            throw DatabaseExceptions.EntryNonExistent<User>(nameof(id), userId.ToString());
        
        return user;
    }
}