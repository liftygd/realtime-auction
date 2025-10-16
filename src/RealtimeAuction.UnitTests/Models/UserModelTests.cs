using RealtimeAuction.Domain.Models;
using RealtimeAuction.Domain.ValueObjects;
using RealtimeAuction.Exceptions;
using RealtimeAuction.Exceptions.Exceptions;
using Xunit;

namespace RealtimeAuction.UnitTests.Models;

public class UserModelTests
{
    [Fact]
    public void UserIdCreation_WhenNullOrEmptyId_ThrowsException()
    {
        //Arrange
        var userId = Guid.Empty;

        //Act
        Action act = () => UserId.Create(userId);

        //Assert
        var exception = Assert.ThrowsAny<ErrorException>(act);
        var entryNonExistentException = ErrorExceptions.NullOrEmpty<UserId>("");
        Assert.Equal(exception.ErrCode, entryNonExistentException.ErrCode);
        Assert.Equal(exception.Caller, entryNonExistentException.Caller);
    }
    
    [Fact]
    public void UserCreation_WhenNullOrEmptyUsername_ThrowsException()
    {
        //Arrange
        var userId = UserId.Create(Guid.NewGuid());
        var userName = string.Empty;
        var email = "1@mail.test";
        var birthday = DateTime.UtcNow;

        //Act
        Action act = () => User.Create(userId, userName, email, birthday);

        //Assert
        var exception = Assert.ThrowsAny<ErrorException>(act);
        var entryNonExistentException = ErrorExceptions.NullOrEmpty<User>("");
        Assert.Equal(exception.ErrCode, entryNonExistentException.ErrCode);
        Assert.Equal(exception.Caller, entryNonExistentException.Caller);
    }
    
    [Fact]
    public void UserCreation_WhenUsernameTooLong_ThrowsException()
    {
        //Arrange
        var userId = UserId.Create(Guid.NewGuid());
        var userName = "123456789ABCDEFGH";
        var email = "1@mail.test";
        var birthday = DateTime.UtcNow;

        //Act
        Action act = () => User.Create(userId, userName, email, birthday);

        //Assert
        var exception = Assert.ThrowsAny<ErrorException>(act);
        var entryNonExistentException = ErrorExceptions.StringTooLong<User>("", User.MAX_USERNAME_LENGTH);
        Assert.Equal(exception.ErrCode, entryNonExistentException.ErrCode);
        Assert.Equal(exception.Caller, entryNonExistentException.Caller);
    }
    
    [Fact]
    public void UserCreation_WhenNullOrEmptyEmail_ThrowsException()
    {
        //Arrange
        var userId = UserId.Create(Guid.NewGuid());
        var userName = "123";
        var email = string.Empty;
        var birthday = DateTime.UtcNow;

        //Act
        Action act = () => User.Create(userId, userName, email, birthday);

        //Assert
        var exception = Assert.ThrowsAny<ErrorException>(act);
        var entryNonExistentException = ErrorExceptions.NullOrEmpty<User>("");
        Assert.Equal(exception.ErrCode, entryNonExistentException.ErrCode);
        Assert.Equal(exception.Caller, entryNonExistentException.Caller);
    }
    
    [Fact]
    public void UserCreation_WhenEmailFormatInvalid_ThrowsException()
    {
        //Arrange
        var userId = UserId.Create(Guid.NewGuid());
        var userName = "123";
        var email = "111@";
        var birthday = DateTime.UtcNow;

        //Act
        Action act = () => User.Create(userId, userName, email, birthday);

        //Assert
        var exception = Assert.ThrowsAny<ErrorException>(act);
        var entryNonExistentException = ErrorExceptions.InvalidFormat<User>("");
        Assert.Equal(exception.ErrCode, entryNonExistentException.ErrCode);
        Assert.Equal(exception.Caller, entryNonExistentException.Caller);
    }
    
    [Fact]
    public void UserCreation_WhenCorrect_CreatesUser()
    {
        //Arrange
        var userId = UserId.Create(Guid.NewGuid());
        var userName = "123";
        var email = "111@mail.test";
        var birthday = DateTime.UtcNow.Date;

        //Act
        var user = User.Create(userId, userName, email, birthday);

        //Assert
        Assert.Equal(userId, user.Id);
        Assert.Equal(userName, user.Username);
        Assert.Equal(email, user.EmailAddress);
        Assert.Equal(birthday, user.Birthday.ToDateTime());
    }
}