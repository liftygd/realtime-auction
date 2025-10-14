using Auction.Domain.Exceptions;

namespace Auction.Domain.ValueObjects;
public record UserId
{
    public Guid Value { get; }

    private UserId(Guid value)
    {
        Value = value;
    }

    public static UserId Create(Guid Id)
    {
        if (Id == Guid.Empty)
            throw ErrorExceptions.NullOrEmpty<UserId>(Id);

        return new UserId(Id);
    }
}
