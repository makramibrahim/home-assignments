using DatabaseEntities;
using DatabaseEntities.Factories;

namespace PersonDemo.API.Injections
{
  public static class CommonInjection
  {
    public static void RegisterCommon(this IServiceCollection services)
    {
      services.AddScoped<IContextFactory, ContextFactory>();
      services.AddScoped<IConnectionFactory, ConnectionFactory>();
      services.AddScoped<IDemoEntities, DemoEntities>();
    }
  }
}
