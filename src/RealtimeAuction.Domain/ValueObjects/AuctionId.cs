using RealtimeAuction.Exceptions.Exceptions;
using RealtimeAuction.Domain.Abstractions;

namespace RealtimeAuction.Domain.ValueObjects;

public record AuctionId : IIdentifierValueObject<Guid>
{
    public Guid Value { get; }

    protected AuctionId() { }

    private AuctionId(Guid value)
    {
        Value = value;
    }

    public static AuctionId Create(Guid value)
    {
        if (value == Guid.Empty)
            throw ErrorExceptions.NullOrEmpty<AuctionId>(nameof(value));

        return new AuctionId(value);
    }
}
