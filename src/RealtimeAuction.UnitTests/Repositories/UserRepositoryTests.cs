using Microsoft.EntityFrameworkCore;
using MockQueryable;
using MockQueryable.Moq;
using Moq;
using RealtimeAuction.Domain.Models;
using RealtimeAuction.Domain.ValueObjects;
using RealtimeAuction.Exceptions;
using RealtimeAuction.Exceptions.Exceptions;
using RealtimeAuction.Infrastructure.Context;
using RealtimeAuction.Infrastructure.Repositories;
using RealtimeAuction.UnitTests.Extensions;
using Xunit;

namespace RealtimeAuction.UnitTests.Repositories;

public class UserRepositoryTests
{
    [Fact]
    public async Task DeleteUser_WhenExists_DeletesAndReturnsTrue()
    {
        //Arrange
        var userId = Guid.NewGuid();
        var data = new List<User>
        {
            User.Create(UserId.Create(userId), "123", "1@mail.test", DateTime.UtcNow),
            User.Create(UserId.Create(Guid.NewGuid()), "123456", "2@mail.test", DateTime.UtcNow)
        };

        var mockSet = data.BuildMockDbSet()
            .SetupObjectRemoving(data);
        
        var mockContext = new Mock<IApplicationDbContext>();
        mockContext.Setup(x => x.Users).Returns(mockSet.Object);

        //Act
        var userRepository = new UserRepository(mockContext.Object);
        var result = await userRepository.DeleteUser(userId);

        //Assert
        Assert.True(result);
        Assert.Single(data);
        Assert.Equal("123456", data[0].Username);
    }
    
    [Fact]
    public async Task DeleteUser_WhenCannotFindUser_ThrowsException()
    {
        //Arrange
        var mockSet = new List<User>().BuildMockDbSet();
        var mockContext = new Mock<IApplicationDbContext>();
        mockContext.Setup(x => x.Users).Returns(mockSet.Object);

        var userRepository = new UserRepository(mockContext.Object);

        //Act
        Func<Task> act = () => userRepository.DeleteUser(Guid.NewGuid());

        //Assert
        var exception = await Assert.ThrowsAnyAsync<ErrorException>(act);
        var entryNonExistentException = DatabaseExceptions.EntryNonExistent<User>("", "");
        Assert.Equal(exception.ErrCode, entryNonExistentException.ErrCode);
        Assert.Equal(exception.Caller, entryNonExistentException.Caller);
    }
}