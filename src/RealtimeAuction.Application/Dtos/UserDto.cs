namespace RealtimeAuction.Application.Dtos;

public record UserDto(Guid Id, string Username, string EmailAddress, DateTime Birthday);