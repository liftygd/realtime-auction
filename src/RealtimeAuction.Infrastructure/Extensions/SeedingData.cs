using RealtimeAuction.Domain.Models;
using RealtimeAuction.Domain.ValueObjects;

namespace RealtimeAuction.Infrastructure.Extensions;

internal class SeedingData
{
    public static IEnumerable<User> Users => 
    new List<User>{
        User.Create(UserId.Create(Guid.NewGuid()), "User1", "1@mail.test", DateTime.UtcNow),
        User.Create(UserId.Create(Guid.NewGuid()), "User2", "2@mail.test", DateTime.UtcNow),
        User.Create(UserId.Create(Guid.NewGuid()), "User3", "3@mail.test", DateTime.UtcNow)
    };
}