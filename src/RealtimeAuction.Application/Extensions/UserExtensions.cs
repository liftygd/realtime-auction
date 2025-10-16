using RealtimeAuction.Application.Dtos;
using RealtimeAuction.Domain.Models;
using RealtimeAuction.Domain.ValueObjects;

namespace RealtimeAuction.Application.Extensions;

public static class UserExtensions
{
    public static List<UserDto> ToUserDtoList(this IEnumerable<User> users)
    {
        var list = new List<UserDto>();

        foreach (var user in users)
            list.Add(user.ToUserDto());
        
        return list;
    }

    public static User ToUser(this UserDto user, Guid userId)
    {
        return User.Create(UserId.Create(userId), user.Username, user.EmailAddress, user.Birthday);
    }

    public static UserDto ToUserDto(this User user)
    {
        return new UserDto(user.Id.Value, user.Username, user.EmailAddress, user.Birthday.ToDateTime());
    }
}