using Microsoft.AspNetCore.Builder;
using RealtimeAuction.Exceptions.Handlers;

namespace RealtimeAuction.Exceptions;

public static class DependencyInjection
{
    public static IApplicationBuilder UseCustomExceptionHandling(this IApplicationBuilder app)
    {
        app.UseMiddleware<CustomExceptionHandlingMiddleware>();

        return app;
    }
}
