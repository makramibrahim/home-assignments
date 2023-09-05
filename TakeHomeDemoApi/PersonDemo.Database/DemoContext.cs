
using Microsoft.EntityFrameworkCore;

namespace DatabaseEntities
{
  public abstract class DemoContext : DbContext
  {
    public string? ErrorMessage { get; set; }
    public string? ConnString { get; }

    //used for Mocking
    protected DemoContext() { }

    protected DemoContext(string? connectionString)
    {
      ConnString  = connectionString;
    }

    public virtual void RefreshEntity<TEntity>(TEntity value)
    {
      if (value != null) Entry(value).Reload();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
      options
        .UseLazyLoadingProxies() // Enable Lazy Loading
        .UseSqlServer(ConnString);
    }

  }
}