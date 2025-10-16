namespace RealtimeAuction.Application.Dtos;

public record UserDto(Guid UserId, string Username, string EmailAddress, DateTime Birthday);