using DatabaseEntities.Factories;
using DatabaseEntities.Factories.Models;

namespace DatabaseEntities.Services
{
  public interface IErrorMessage
  {
    public string? ErrorMessage { get; set; }
  }

  public class BaseService : IErrorMessage
  {
    protected readonly IContextFactory ContextFactory;

    protected BaseService(IContextFactory factory)
    {
      ContextFactory = factory;
    }

    public string? ErrorMessage { get; set; }

    protected TEntity DbContext<TEntity>(DatabaseName db = DatabaseName.DemoDb) where TEntity 
      : DemoContext
    {
      return ContextFactory.DbContext<TEntity>(db);
    }

    protected DemoEntities GetDataBaseContext()
    {
      return DbContext<DemoEntities>();
    }

  }
}