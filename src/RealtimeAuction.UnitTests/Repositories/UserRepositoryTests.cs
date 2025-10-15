using Microsoft.EntityFrameworkCore;
using Moq;
using RealtimeAuction.Domain.Models;
using RealtimeAuction.Exceptions;
using RealtimeAuction.Exceptions.Exceptions;
using RealtimeAuction.Infrastructure;
using RealtimeAuction.Infrastructure.Repositories;
using Xunit;

namespace RealtimeAuction.UnitTests.Repositories;

public class UserRepositoryTests
{
    [Fact]
    public void DeleteUser_ThrowsExceptionWhenCannotFindUser()
    {
        //Arrange
        var mockSet = new Mock<DbSet<User>>();
        var mockContext = new Mock<ApplicationDbContext>();
        mockContext.Setup(x => x.Users).Returns(mockSet.Object);

        var userRepository = new UserRepository(mockContext.Object);

        //Act
        Action act = () => userRepository.DeleteUser(Guid.NewGuid());

        //Assert
        var exception = Assert.Throws<ErrorException>(act);
        var entryNonExistentException = DatabaseExceptions.EntryNonExistent<User>("", "");
        Assert.Equal(exception.ErrCode, entryNonExistentException.ErrCode);
    }
}