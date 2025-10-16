namespace RealtimeAuction.Application.Abstractions;

public interface ICommand<out TResponse> : IRequest<TResponse>
{
    
}