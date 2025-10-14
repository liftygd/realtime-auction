namespace Auction.Domain.Abstractions;
public interface IEntity<T>
{
    public T Id { get; }
}
