using RealtimeAuction.Exceptions.Exceptions;
using RealtimeAuction.Domain.Abstractions;

namespace RealtimeAuction.Domain.ValueObjects;

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
