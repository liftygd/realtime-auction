using RealtimeAuction.Application.Dtos;
using RealtimeAuction.Domain.Models;

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

    public static UserDto ToUserDto(this User user)
    {
        return new UserDto(user.Id.Value, user.Username, user.EmailAddress, user.Birthday.ToDateTime());
    }
}