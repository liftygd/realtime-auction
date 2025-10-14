using Auction.Domain.Abstractions;
using Auction.Domain.ValueObjects;
using Auction.Exceptions.Exceptions;

namespace Auction.Domain.Models;

public class User : Entity<UserId>
{
    public const int MAX_USERNAME_LENGTH = 16;

    public string Username { get; private set; }
    public string EmailAddress { get; private set; }

    private User(
        string userName,
        string emailAddress)
    {
        Username = userName;
        EmailAddress = emailAddress;
    }

    public static User Create(string userName, string emailAddress)
    {
        if (string.IsNullOrEmpty(userName))
            throw ErrorExceptions.NullOrEmpty<User>(nameof(userName));

        if (userName.Length > MAX_USERNAME_LENGTH)
            throw ErrorExceptions.InvalidLength<User>(nameof(userName));

        if (string.IsNullOrEmpty(emailAddress))
            throw ErrorExceptions.NullOrEmpty<User>(nameof(emailAddress));

        return new User(userName, emailAddress);
    }
}
