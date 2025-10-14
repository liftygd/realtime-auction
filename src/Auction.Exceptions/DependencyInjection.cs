using Auction.Exceptions.Handlers;
using Microsoft.AspNetCore.Builder;

namespace Auction.Exceptions;

public static class DependencyInjection
{
    public static IApplicationBuilder UseCustomExceptionHandling(this IApplicationBuilder app)
    {
        app.UseMiddleware<CustomExceptionHandlingMiddleware>();

        return app;
    }
}
