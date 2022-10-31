using System.Net;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using GlobalErrorApp.Exceptions;

namespace GlobalErrorApp.Configurations;

public class GlobalExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

    public GlobalExceptionHandlingMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        HttpStatusCode status;
        string message = "";
        _logger.LogError(ex.StackTrace);

        var type = ex.GetType();

        if (type == typeof(NotFoundException))
        {
            message = ex.Message;
            status = HttpStatusCode.NotFound;
        }
        else if (type == typeof(BadRequestException))
        {
            message = ex.Message;
            status = HttpStatusCode.BadRequest;
        }
        else if (type == typeof(Exceptions.NotImplementedException))
        {
            message = ex.Message;
            status = HttpStatusCode.NotImplemented;
        }
        else if (type == typeof(Exceptions.KeyNotFoundException))
        {
            message = ex.Message;
            status = HttpStatusCode.NotFound;
        }
        else
        {
            message = ex.Message;
            status = HttpStatusCode.InternalServerError;
        }

        var options = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
            WriteIndented = true
        };
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)status;
        var result = JsonSerializer.Serialize(new { message = message, status = status, isError = true }, options);
        
        return context.Response.WriteAsync(result);
    }
}