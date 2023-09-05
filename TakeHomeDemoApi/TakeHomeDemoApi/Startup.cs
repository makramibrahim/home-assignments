using DatabaseEntities;
using DatabaseEntities.Factories;
using Microsoft.EntityFrameworkCore;
using PersonDemo.API.Injections;
using PersonDemo.API.Middleware;

namespace PersonDemo.API
{
  public class Startup
  {
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddCors(options =>
      {
        options.AddDefaultPolicy(builder =>
        {
          builder
            .SetIsOriginAllowedToAllowWildcardSubdomains()
            .SetIsOriginAllowed((origin) =>
            {
              var host = new Uri(origin).Host;
              return (host.Contains("localhost")
                      || host.Contains("mak-demo-api.com", StringComparison.InvariantCultureIgnoreCase));
            })
            .AllowAnyHeader()
            .AllowAnyMethod()
            .WithExposedHeaders("ErrorMessage", "X-Auth-Token");
        });
      });
      
      IConfigurationSection settings = Configuration.GetSection("AppSettings");
      services.Configure<AppSettings>(settings);

      services.AddDbContext<DemoEntities>(options => 
        options.UseSqlServer(Configuration.GetConnectionString("DemoConnectionString"),
          b => b.MigrationsAssembly("PersonDemo.API")));
      services.AddEndpointsApiExplorer();
      services.AddHttpContextAccessor();
      services.RegisterCommon();
      services.RegisterPerson();
      services.AddSwaggerGen();
      services.AddControllers();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseSwagger();
        app.UseDeveloperExceptionPage();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mak-Demo.API v1"));
      }

      app.UseHttpsRedirection();
      app.UseCors();
      app.UseRouting();
      //Custom Middlewares
      app.UseCustomExceptionHandler();
      app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
  }
}
