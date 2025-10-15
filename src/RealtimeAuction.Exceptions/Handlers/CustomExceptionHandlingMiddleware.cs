using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using RealtimeAuction.Exceptions.Exceptions;

namespace RealtimeAuction.Exceptions.Handlers;

public class CustomExceptionHandlingMiddleware(
    RequestDelegate next,
    ILogger<CustomExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await next.Invoke(httpContext);
        }
        catch (ErrorException err)
        {
            await HandleExceptionAsync(httpContext, err);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, ErrorException err)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = StatusCodes.Status400BadRequest;

        var errCode = err.ErrCode;
        var message = err.ErrMessage;
        var caller = err.Caller;

        var errorResponse = new ErrorResponse(errCode, caller, message);
        logger.LogError($"Handled error exception with code '{errCode}' from '{caller}' with message: '{message}'");

        return context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
    }
}
