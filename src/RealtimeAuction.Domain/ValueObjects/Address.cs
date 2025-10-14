using RealtimeAuction.Exceptions.Exceptions;

namespace RealtimeAuction.Domain.ValueObjects;

public record Address
{
    public string AddressLine { get; }
    public string Country { get; } = string.Empty;
    public string State { get; } = string.Empty;
    public string ZipCode { get; } = string.Empty;

    protected Address()
    {
    }

    private Address(string addressLine, string country, string state, string zipCode)
    {
        AddressLine = addressLine;
        Country = country;
        State = state;
        ZipCode = zipCode;
    }

    public static Address Of(string addressLine, string country, string state, string zipCode)
    {
        if (string.IsNullOrWhiteSpace(addressLine))
            throw ErrorExceptions.NullOrEmpty<Address>(nameof(addressLine));

        return new Address(addressLine, country, state, zipCode);
    }
}
