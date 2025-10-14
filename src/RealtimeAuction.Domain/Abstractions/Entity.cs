namespace RealtimeAuction.Domain.Abstractions;
public class Entity<T> : IEntity<T>
{
    public T Id { get; set; }
}
