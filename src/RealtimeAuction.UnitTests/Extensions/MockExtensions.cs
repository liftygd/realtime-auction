using Microsoft.EntityFrameworkCore;
using Moq;
using RealtimeAuction.Domain.Abstractions;

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
    
    public static Mock<DbSet<TSetType>> SetupObjectCreation<TSetType>(this Mock<DbSet<TSetType>> mockDbSet, List<TSetType> objects) 
        where TSetType : class
    {
        mockDbSet.Setup(m => m.Add(It.IsAny<TSetType>()))
            .Callback<TSetType>((entity) => objects.Add(entity));
        
        return mockDbSet;
    }
    
    public static Mock<DbSet<TSetType>> SetupObjectUpdating<TSetType, TIdType>(this Mock<DbSet<TSetType>> mockDbSet, List<TSetType> objects) 
        where TSetType : Entity<TIdType>
        where TIdType : IIdentifierValueObject<Guid>
    {
        mockDbSet.Setup(m => m.Update(It.IsAny<TSetType>()))
            .Callback<TSetType>((entity) =>
            {
                var objIndex = objects.FindIndex(x => x.Id.Value == entity.Id.Value);
                if (objIndex == -1)
                    return;
                
                objects[objIndex] = entity;
            });
        
        return mockDbSet;
    }
}