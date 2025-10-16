namespace RealtimeAuction.Application.Abstractions;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
    
}