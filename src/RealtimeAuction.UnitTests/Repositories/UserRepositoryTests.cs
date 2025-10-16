using Microsoft.EntityFrameworkCore;
using MockQueryable;
using MockQueryable.Moq;
using Moq;
using Npgsql;
using RealtimeAuction.Domain.Models;
using RealtimeAuction.Domain.ValueObjects;
using RealtimeAuction.Exceptions;
using RealtimeAuction.Exceptions.Exceptions;
using RealtimeAuction.Infrastructure.Context;
using RealtimeAuction.Infrastructure.Repositories;
using RealtimeAuction.UnitTests.Extensions;
using RealtimeAuction.UnitTests.Helpers;
using Xunit;

namespace RealtimeAuction.UnitTests.Repositories;

public class UserRepositoryTests
{
    [Fact]
    public async Task CreateUser_WhenDoesntExist_CreatesNewUser()
    {
        //Arrange
        var data = new List<User>();

        var mockSet = data.BuildMockDbSet()
            .SetupObjectCreation(data);

        var mockContext = new Mock<IApplicationDbContext>();
        mockContext.Setup(x => x.Users).Returns(mockSet.Object);

        var userId = UserId.Create(Guid.NewGuid());
        var newUser = User.Create(userId, "newUser", "1@mail.test", DateTime.UtcNow);
        
        //Act
        var userRepository = new UserRepository(mockContext.Object);
        var resultId = await userRepository.CreateUser(newUser);
        
        //Assert
        Assert.Single(data);
        Assert.Equal(userId.Value, resultId);
    }
    
    [Fact]
    public async Task CreateUser_WhenNonUnique_ThrowsException()
    {
        //Arrange
        var data = new List<User>
        {
            User.Create(UserId.Create(Guid.NewGuid()), "123", "1@mail.test", DateTime.UtcNow),
            User.Create(UserId.Create(Guid.NewGuid()), "123456", "2@mail.test", DateTime.UtcNow)
        };
        var mockSet = data.BuildMockDbSet()
            .SetupObjectCreation(data);

        var mockContext = new Mock<IApplicationDbContext>();
        mockContext.Setup(x => x.Users).Returns(mockSet.Object);
        mockContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ThrowsAsync(PostgresExceptionHelper.CreateUniqueViolationException());

        var newUser1 = User.Create(UserId.Create(Guid.NewGuid()), "123", "1@mail.test", DateTime.UtcNow);
        var newUser2 = User.Create(UserId.Create(Guid.NewGuid()), "000", "2@mail.test", DateTime.UtcNow);
        
        //Act
        var userRepository = new UserRepository(mockContext.Object);
        Func<Task> act1 = () => userRepository.CreateUser(newUser1);
        Func<Task> act2 = () => userRepository.CreateUser(newUser2);
        
        //Assert
        var entryNotUnique = DatabaseExceptions.EntryValueNotUnique<User>("");
        var exception1 = await Assert.ThrowsAnyAsync<ErrorException>(act1);
        var exception2 = await Assert.ThrowsAnyAsync<ErrorException>(act2);
        
        Assert.Equal(exception1.ErrCode, entryNotUnique.ErrCode);
        Assert.Equal(exception1.Caller, entryNotUnique.Caller);
        
        Assert.Equal(exception2.ErrCode, entryNotUnique.ErrCode);
        Assert.Equal(exception2.Caller, entryNotUnique.Caller);
    }
    
    [Fact]
    public async Task UpdateUser_WhenExists_UpdatesUserValues()
    {
        //Arrange
        var userId = UserId.Create(Guid.NewGuid());
        var data = new List<User>
        {
            User.Create(userId, "123", "1@mail.test", DateTime.UtcNow),
            User.Create(UserId.Create(Guid.NewGuid()), "123456", "2@mail.test", DateTime.UtcNow)
        };

        var mockSet = data.BuildMockDbSet()
            .SetupObjectUpdating<User, UserId>(data);

        var mockContext = new Mock<IApplicationDbContext>();
        mockContext.Setup(x => x.Users).Returns(mockSet.Object);

        var newUser = User.Create(userId, "newUser", "3@mail.test", DateTime.UtcNow);
        
        //Act
        var userRepository = new UserRepository(mockContext.Object);
        var result = await userRepository.UpdateUser(userId, newUser);
        var updatedUser = data[0];
        
        //Assert
        Assert.True(result);
        Assert.Equal(2, data.Count);
        Assert.Equal("newUser", updatedUser.Username);
        Assert.Equal("3@mail.test", updatedUser.EmailAddress);
    }
    
    [Fact]
    public async Task GetUserById_WhenCannotFindUser_ThrowsException()
    {
        //Arrange
        var mockSet = new List<User>().BuildMockDbSet();
        var mockContext = new Mock<IApplicationDbContext>();
        mockContext.Setup(x => x.Users).Returns(mockSet.Object);

        var userRepository = new UserRepository(mockContext.Object);

        //Act
        Func<Task> act = () => userRepository.GetUserById(UserId.Create(Guid.NewGuid()));

        //Assert
        var exception = await Assert.ThrowsAnyAsync<ErrorException>(act);
        var entryNonExistentException = DatabaseExceptions.EntryNonExistent<User>("", "");
        Assert.Equal(exception.ErrCode, entryNonExistentException.ErrCode);
        Assert.Equal(exception.Caller, entryNonExistentException.Caller);
    }
    
    [Fact]
    public async Task UpdateUser_WhenCannotFindUser_ThrowsException()
    {
        //Arrange
        var mockSet = new List<User>().BuildMockDbSet();
        var mockContext = new Mock<IApplicationDbContext>();
        mockContext.Setup(x => x.Users).Returns(mockSet.Object);

        var userRepository = new UserRepository(mockContext.Object);

        //Act
        Func<Task> act = () => userRepository.UpdateUser(UserId.Create(Guid.NewGuid()), null!);

        //Assert
        var exception = await Assert.ThrowsAnyAsync<ErrorException>(act);
        var entryNonExistentException = DatabaseExceptions.EntryNonExistent<User>("", "");
        Assert.Equal(exception.ErrCode, entryNonExistentException.ErrCode);
        Assert.Equal(exception.Caller, entryNonExistentException.Caller);
    }
    
    [Fact]
    public async Task DeleteUser_WhenExists_DeletesAndReturnsTrue()
    {
        //Arrange
        var userId = UserId.Create(Guid.NewGuid());
        var data = new List<User>
        {
            User.Create(userId, "123", "1@mail.test", DateTime.UtcNow),
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
        Func<Task> act = () => userRepository.DeleteUser(UserId.Create(Guid.NewGuid()));

        //Assert
        var exception = await Assert.ThrowsAnyAsync<ErrorException>(act);
        var entryNonExistentException = DatabaseExceptions.EntryNonExistent<User>("", "");
        Assert.Equal(exception.ErrCode, entryNonExistentException.ErrCode);
        Assert.Equal(exception.Caller, entryNonExistentException.Caller);
    }
}