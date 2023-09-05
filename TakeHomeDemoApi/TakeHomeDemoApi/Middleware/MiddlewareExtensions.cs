namespace PersonDemo.API.Middleware
{
  public static class MiddlewareExtensions
  {
    public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder app)
    {
      return app.UseMiddleware<ExceptionHandler>();
    }
  }
}