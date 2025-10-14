using Auction.Exceptions.Exceptions;

namespace Auction.Domain.ValueObjects;
public record UserId
{
    public Guid Value { get; }

    protected UserId() { }
    private UserId(Guid value)
    {
        Value = value;
    }

    public static UserId Create(Guid Id)
    {
        if (Id == Guid.Empty)
            throw ErrorExceptions.NullOrEmpty<UserId>(nameof(Id));

        return new UserId(Id);
    }
}
