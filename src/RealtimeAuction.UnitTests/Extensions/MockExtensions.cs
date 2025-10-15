using Microsoft.EntityFrameworkCore;
using Moq;

namespace RealtimeAuction.UnitTests.Extensions;

internal static class MockExtensions
{
    public static Mock<DbSet<TSetType>> SetupObjectRemoving<TSetType>(this Mock<DbSet<TSetType>> mockDbSet, List<TSetType> objects) 
        where TSetType : class
    {
        mockDbSet.Setup(m => m.Remove(It.IsAny<TSetType>()))
            .Callback<TSetType>((entity) => objects.Remove(entity));
        
        return mockDbSet;
    }
}