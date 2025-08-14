using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace BankingSystem.Core.Common;

/// <summary>
/// Custom Middleware for handling global exceptions
/// </summary>
public class ExecutionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExecutionMiddleware> _logger;
    public ExecutionMiddleware(RequestDelegate next, ILogger<ExecutionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            var start = DateTime.UtcNow;
            await _next(context);
            var elapsed = DateTime.UtcNow - start;
            _logger.LogInformation($"{context.Request.Path} completed execution in {elapsed}ms.");
        }
        catch(AppException ex)
        {
            await HandleExceptionAsync(context, ex.Message, ex.StatusCode ?? StatusCodes.Status500InternalServerError);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.ToString(), ex);
            await HandleExceptionAsync(context, "Something wrong happen, Please try again later.", StatusCodes.Status500InternalServerError);
        }
    }
    private async Task HandleExceptionAsync(HttpContext context, string message, int statusCode)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        var response = new
        {
            success = false,
            error = message
        };

        var json = JsonSerializer.Serialize(response);
        await context.Response.WriteAsync(json);
    }
}
