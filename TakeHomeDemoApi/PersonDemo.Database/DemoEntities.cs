using Microsoft.EntityFrameworkCore;
using DatabaseEntities.Data;

namespace DatabaseEntities
{
  public interface IDemoEntities
  {
    DbSet<Person>? Persons { get; set; }
    DbSet<PersonVersion>? PersonVersions { get; set; }
  }


  public class DemoEntities : DemoContext, IDemoEntities
  {
    public DemoEntities(string? connectionString) : base(connectionString)
    {
    }

    public DemoEntities()
    {
    }

    public DbSet<Person>? Persons { get; set; }
    public DbSet<PersonVersion>? PersonVersions { get; set; }

  }
}
