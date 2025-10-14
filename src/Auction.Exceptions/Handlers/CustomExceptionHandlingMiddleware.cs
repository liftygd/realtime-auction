using Auction.Exceptions.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Auction.Exceptions.Handlers;

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

        var error = err.Message.Split(" || ");
        if (error.Length < 3)
            return Task.CompletedTask;

        var errCode = error[0];
        var message = error[1];
        var caller = error[2];

        var errorResponse = new ErrorResponse(errCode, caller, message);
        logger.LogError($"Handled error exception with code '{errCode}' from '{caller}' with message: '{message}'");

        return context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
    }
}
