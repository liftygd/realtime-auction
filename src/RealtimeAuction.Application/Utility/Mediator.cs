using RealtimeAuction.Application.Abstractions;

namespace RealtimeAuction.Application.Utility;

public class Mediator(IServiceProvider serviceProvider) : IMediator
{
    public async Task<TResponse> Send<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default) where TRequest : IRequest<TResponse>
    {
        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(typeof(TRequest), typeof(TResponse));

        dynamic? handler = serviceProvider.GetService(handlerType);
        if (handler == null)
            throw new NotImplementedException($"Handler not found for request of type '{request.GetType().Name}'.");
        
        return await handler.Handle((dynamic) request, cancellationToken);
    }
}