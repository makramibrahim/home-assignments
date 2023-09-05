namespace DatabaseEntities.Factories
{
  public interface IConnectionFactory
  {
    TEntity GetConnection<TEntity>()
      where TEntity : DemoEntities, new();
  }

  public class ConnectionFactory : IConnectionFactory
  {
    public TEntity GetConnection<TEntity>()
      where TEntity : DemoEntities, new()
    {
      return new();
    }
  }
}