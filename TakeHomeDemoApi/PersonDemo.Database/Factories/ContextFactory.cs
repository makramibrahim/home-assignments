using DatabaseEntities.Factories.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using NLog;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text;

namespace DatabaseEntities.Factories
{
  public interface IContextFactory
  {
    IHttpContextAccessor HttpContext { get; }
    string? CustomConnectionString { get; set; }
    string DisplayUrl { get; }
    AppSettings? Settings { get; }
    string? LoggingConnString { get; }
    TEntity DbContext<TEntity>(DatabaseName db = DatabaseName.DemoDb)
      where TEntity : DemoContext;
    string? ConnectionString(DatabaseName db);
    IDemoEntities GetDataBaseContext();
  }

  /// <summary>
  ///   Excluding from Code Coverage as the class itself isn't testable.
  ///   Used as a wrapper for HttpContext.Current and for establishing new database connections
  /// </summary>
  [ExcludeFromCodeCoverage]
  public class ContextFactory : IContextFactory
  {
    private readonly IConfiguration _configuration;
    private readonly Dictionary<DatabaseName, string?> _connectionStrings = new();
    private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

    public ContextFactory(IHttpContextAccessor contextAccessor, IConfiguration configuration,
      IOptions<AppSettings> settings)
    {
      HttpContext = contextAccessor;
      _configuration = configuration;
      Settings = settings.Value;
      try
      {
        _connectionStrings.Add(DatabaseName.DemoDb, configuration.GetConnectionString("DemoConnectionString"));
        using DemoEntities context = DbContext<DemoEntities>();
      }
      catch (Exception e)
      {
        // log error
        StringBuilder message = new();
        while (e.Message != null!)
        {
          message.AppendLine(e.Message);
          e = e.InnerException!;
        }
        message.AppendLine($"ContextFactory: {e.Message}");
        _logger.Error(e, "An error occurred: {0}", message);
      }
    }
    public string? CustomConnectionString { get; set; }
    public IHttpContextAccessor HttpContext { get; }
    public AppSettings? Settings { get; }

    public string DisplayUrl => HttpContext.HttpContext.Request.GetDisplayUrl();

    private string? _loggingConnString;

    public string? LoggingConnString
    {
      get
      {
        if (string.IsNullOrEmpty(_loggingConnString))
        {
          _loggingConnString = _configuration.GetConnectionString("LoggingConnection");
        }

        return _loggingConnString;
      }
    }

    public IDemoEntities GetDataBaseContext()
    {
      return DbContext<DemoEntities>();
    }

    public TEntity DbContext<TEntity>(DatabaseName db = DatabaseName.DemoDb) where TEntity : DemoContext
    {
      string? connString = _configuration.GetConnectionString("DemoConnectionString");
      if (!string.IsNullOrEmpty(CustomConnectionString) && db != DatabaseName.DemoDb)
      {
        connString = CustomConnectionString;
      }
      else if (_connectionStrings.TryGetValue(db, out string? connectionString))
      {
        connString = connectionString;
      }

      if (connString != null && !connString.Contains("Trust Server Certificate=true"))
      {
        connString += $"{(connString.Trim().EndsWith(";") ? "" : ";")}Trust Server Certificate=true";
      }

      ConstructorInfo? constructor = typeof(TEntity).GetConstructor(new[] {typeof(string)});
      if (constructor == null)
      {
        throw new NotImplementedException(
          $"Unable to create context of type {typeof(TEntity)} due to missing constructor.");
      }
      object output = constructor.Invoke(new object[] { connString! });
      return (output as TEntity)!;
    }

    public string? ConnectionString(DatabaseName db)
    {
      return _connectionStrings.TryGetValue(db, out string? connectionString)
        ? connectionString
        : _configuration.GetConnectionString("DemoConnectionString");
    }
  }
}