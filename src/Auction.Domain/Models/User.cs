using System.Text.RegularExpressions;
using Auction.Domain.Abstractions;
using Auction.Domain.ValueObjects;
using Auction.Exceptions.Exceptions;

namespace Auction.Domain.Models;

public class User : Entity<UserId>
{
    public const int MAX_USERNAME_LENGTH = 16;
    public const string EMAIL_FORMAT = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

    public string Username { get; private set; }
    public string EmailAddress { get; private set; }
    public Birthday? Birthday { get; private set; }

    public static User Create(
        string userName, 
        string emailAddress,
        DateTime? birthday = null)
    {
        if (string.IsNullOrEmpty(userName))
            throw ErrorExceptions.NullOrEmpty<User>(nameof(userName));

        if (userName.Length > MAX_USERNAME_LENGTH)
            throw ErrorExceptions.StringTooLong<User>(nameof(userName), MAX_USERNAME_LENGTH);

        if (string.IsNullOrEmpty(emailAddress))
            throw ErrorExceptions.NullOrEmpty<User>(nameof(emailAddress));
        
        if (!Regex.IsMatch(emailAddress, EMAIL_FORMAT, RegexOptions.IgnoreCase))
            throw ErrorExceptions.InvalidFormat<User>(nameof(emailAddress));
        
        var user = new User
        {
            Username = userName,
            EmailAddress = emailAddress,
            Birthday = birthday != null ? Birthday.Create(birthday.Value.Year, birthday.Value.Month, birthday.Value.Day) : null
        };
        
        return user;
    }
}
