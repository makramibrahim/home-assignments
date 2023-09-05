using PersonDemo.API.Services;
using PersonDemo.API.Services.interfaces;

namespace PersonDemo.API.Injections
{
  public static class PersonInjection
  {
    public static void RegisterPerson(this IServiceCollection services)
    {
      services.AddScoped<IPersonService, PersonService>();
    }
  }
}
