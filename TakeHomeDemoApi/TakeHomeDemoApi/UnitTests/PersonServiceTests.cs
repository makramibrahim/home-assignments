using DatabaseEntities;
using DatabaseEntities.Data;
using DatabaseEntities.Enums;
using DatabaseEntities.Factories;
using Moq;
using NUnit.Framework;
using PersonDemo.API.Models;
using PersonDemo.API.Models.Enums;
using PersonDemo.API.Services;
using PersonDemo.API.Services.interfaces;

namespace PersonDemo.API.UnitTests
{
  [TestFixture]
  public class PersonServiceTests
  {
    private IPersonService? _service;
    private Mock<IContextFactory>? _factory;
    private Mock<IDemoEntities>? _context;
    private Mock<DemoEntities>? context;

    [SetUp]
    public void SetUp()
    {
      _service = new PersonService(_factory!.Object);
    }

    [TearDown]
    public void TearDown()
    {
      _context!.Invocations.Clear();
    }


    [OneTimeSetUp]
    public void OnTimeSetup()
    {
      _factory = new Mock<IContextFactory>();
      _context = new Mock<IDemoEntities>();
      Guid personId = Guid.NewGuid();

      _context.Setup(ctx => ctx.Persons)
        .Returns(() =>
        {
          var data = new List<Person>
          {
             new()
             {
               Id = personId,
               GivenName = "Mak",
               BirthDate = DateTime.Now.AddYears(-38),
               Gender = Gender.Male,
               Versions = new List<PersonVersion>
               {
                 new()
                 {
                   Id = 1,
                   PersonId = personId,
                   GivenName = "Mak",
                   BirthDate = DateTime.Now.AddYears(-38),
                   Gender = Gender.Male,
                 }
               }
             }
          };
          return data.MockDataSet().Object;
        });

      _context.Setup(ctx => ctx.PersonVersions).Returns(() =>
      {
        var data = new List<PersonVersion>
        {
            new()
            {
              Id = 1,
              PersonId = personId,
              GivenName = "Mak",
              BirthDate = DateTime.Now.AddYears(-35),
              Gender = Gender.Male
            }
        };

        return data.MockDataSet().Object;
      });

      var service = new Mock<IPersonService>();
      service.Setup(s => s.CreatePerson(It.IsAny<DPerson>())).Returns(true);
      service.Setup(s => s.UpdatePerson(It.IsAny<DPerson>())).Returns(true);
    }


    [Test]
    public void Create_Return_True_WhenSuccessful()
    {
      Assert.That(_service!.CreatePerson(new DPerson
      {
        GivenName = "Mak",
        Id = Guid.NewGuid(),
        BirthDate = DateTime.Now.AddYears(-38),
        Gender = (DGender)Gender.Male
      }), Is.True);
    }

    [Test]
    public void Create_Return_False_WhenFail()
    {
      Assert.That(_service!.CreatePerson(new DPerson()), 
        Is.False.And.EqualTo(_service.ErrorMessage));
    }
  }
}
