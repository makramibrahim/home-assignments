using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;

namespace DatabaseEntities.Factories
{
  public static partial class Extensions
  {
    public static Mock<DbSet<T>> MockDataSet<T>(this IEnumerable<T> data) where T : class
    {
      return data.AsQueryable().BuildMockDbSet();
    }
  }
}
