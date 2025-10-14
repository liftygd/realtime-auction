using Auction.Domain.Abstractions;
using Auction.Exceptions.Exceptions;

namespace Auction.Domain.ValueObjects;

public record UserId : IIdentifierValueObject<Guid>
{
    public Guid Value { get; }

    protected UserId() { }

    private UserId(Guid value)
    {
        Value = value;
    }

    public static UserId Create(Guid value)
    {
        if (value == Guid.Empty)
            throw ErrorExceptions.NullOrEmpty<UserId>(nameof(value));

        return new UserId(value);
    }
}
