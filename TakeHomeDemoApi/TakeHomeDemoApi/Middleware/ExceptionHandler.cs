using Microsoft.Extensions.Primitives;
using NLog;
using System.Net;
using System.Text;

namespace PersonDemo.API.Middleware
{
  public class ExceptionHandler
  {
    private readonly List<string> _importantHeaders = new()
    {
      "X-System",
      "X-User-Type"
    };

    private readonly RequestDelegate _next;
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();


    public ExceptionHandler(RequestDelegate next)
    {
      _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
      try
      {
        await _next(context);
      }
      catch (Exception e)
      {
        IEnumerable<KeyValuePair<string, StringValues>> headers = context.Request.Headers.Where(h =>
          _importantHeaders.Contains(h.Key, StringComparer.InvariantCultureIgnoreCase));
        string origin = context.Request.Headers["Origin"]!;
        string referer = context.Request.Headers["Referer"]!;
        int? systemAccessId = int.TryParse(context.Request.Headers["X-System"], out var system) ? system : null;
        
        // log error
        var message = new StringBuilder();
        while (e != null!)
        {
          message.AppendLine(e.Message);
          e = e.InnerException!;
        }
        message.AppendLine($"origin: {origin}; referer: {referer}; systemAccessId: {systemAccessId}; RequestHeaders: {headers}");
        _logger.Error(e, "An error occurred: {0}", message);
        await HandleExceptionAsync(context);
      }
    }

    private static Task HandleExceptionAsync(HttpContext context)
    {
      context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
      context.Response.ContentType = "application/json";
      return Task.CompletedTask;
    }
  }
}