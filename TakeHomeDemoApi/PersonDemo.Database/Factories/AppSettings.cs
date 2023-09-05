using System.Diagnostics.CodeAnalysis;

namespace DatabaseEntities.Factories
{
  [ExcludeFromCodeCoverage]
  public class AppSettings
  {
    public string? ConnectionString { get; set; }
  }
}
