using RealtimeAuction.Exceptions.Exceptions;
using RealtimeAuction.Domain.Abstractions;

namespace RealtimeAuction.Domain.ValueObjects;

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
