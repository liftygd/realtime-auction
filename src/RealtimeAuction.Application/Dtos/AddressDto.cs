namespace RealtimeAuction.Application.Dtos;

public record AddressDto(
    string AddressLine,
    string Country,
    string State,
    string ZipCode);