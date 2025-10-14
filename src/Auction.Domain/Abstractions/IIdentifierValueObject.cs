namespace Auction.Domain.Abstractions;

public interface IIdentifierValueObject<TValueType>
{
    TValueType Value { get; }
}
