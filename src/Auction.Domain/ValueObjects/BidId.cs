using Auction.Domain.Abstractions;
using Auction.Exceptions.Exceptions;

namespace Auction.Domain.ValueObjects;

public record BidId : IIdentifierValueObject<Guid>
{
    public Guid Value { get; }

    protected BidId() { }

    private BidId(Guid value)
    {
        Value = value;
    }

    public static BidId Create(Guid value)
    {
        if (value == Guid.Empty)
            throw ErrorExceptions.NullOrEmpty<BidId>(nameof(value));

        return new BidId(value);
    }
}
