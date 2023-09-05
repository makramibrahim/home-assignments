using DatabaseEntities;
using DatabaseEntities.Factories;
using NUnit.Framework;

namespace PersonDemo.API.UnitTests
{
  [TestFixture]
  public class ConnectionFactoryTests
  {
    [Test]
    public void GetConnection_ReturnsFactory()
    {
      ConnectionFactory factory = new ();
      Assert.That(factory.GetConnection<DemoEntities>(),
        Is.Not.Null.And.TypeOf<DemoEntities>());
    }
  }
}
