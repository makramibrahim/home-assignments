using DatabaseEntities;
using DatabaseEntities.Data;
using DatabaseEntities.Enums;
using DatabaseEntities.Factories;
using DatabaseEntities.Services;
using Microsoft.EntityFrameworkCore;
using PersonDemo.API.Models;
using PersonDemo.API.Models.Enums;
using PersonDemo.API.Services.interfaces;

namespace PersonDemo.API.Services
{
  public class PersonService : BaseService, IPersonService
  {
    public PersonService(IContextFactory factory) : base(factory) { }

    public bool CreatePerson(DPerson? person)

    {
      if (person == null)
      {
        ErrorMessage = "A request to create a person can't be empty";
        return false;
      }

      using DemoEntities context = GetDataBaseContext();
      Person? exists = context.Persons!.FirstOrDefault(p => p.Id == person.Id);

      if (exists == null)
      {
        exists = new Person
        {
          Id = Guid.NewGuid(),
          BirthDate = person.BirthDate,
          BirthLocation = person.BirthLocation,
          DeathDate = person.DeathDate,
          DeathLocation = person.DeathLocation,
          Gender = (Gender)person.Gender,
          GivenName = person.GivenName,
          Surname = person.Surname
        };
        context.Persons!.Add(exists);
        context.SaveChanges();
        return true;

      }
      ErrorMessage = $"{person.GivenName} {person.Surname} exists in the system";
      return false;
    }

    public bool UpdatePerson(DPerson person)
    {
      using DemoEntities context = GetDataBaseContext();
      Person? exists = context.Persons!.FirstOrDefault(p => p.Id == person.Id);
      if (exists == null)
      {
        ErrorMessage = "Person is not found";
        return false;
      }
      exists.BirthDate = person.BirthDate;
      exists.BirthLocation = person.BirthLocation;
      exists.DeathDate = person.DeathDate;
      exists.DeathLocation = person.DeathLocation;
      exists.Gender = (Gender)person.Gender;
      exists.GivenName = person.GivenName;
      exists.Surname = person.Surname;
      exists.Versions!.Add(new PersonVersion
      {
        PersonId = exists.Id,
        BirthDate = person.BirthDate,
        BirthLocation = person.BirthLocation,
        DeathDate = person.DeathDate,
        DeathLocation = person.DeathLocation,
        Gender = (Gender)person.Gender,
        GivenName = person.GivenName,
        Surname = person.Surname,
        Timestamp = DateTime.Now
      });
      context.SaveChanges();
      return true;
    }

    public DPerson GetPerson(Guid id)
    {
      using DemoEntities context = GetDataBaseContext();
      Person? person = context.Persons!.FirstOrDefault(p => p.Id == id);
      return CommonConfig.AutoMapper.Mapper.Map<DPerson>(person);
    }

    public List<DPerson> GetAll()
    {
      using DemoEntities context = GetDataBaseContext();
      List<Person> persons = context.Persons!
        .Include(p => p.Versions).ToList();
      return CommonConfig.AutoMapper.Mapper.Map<List<DPerson>>(persons);
    }

    public bool Delete(Guid id)
    {
      using DemoEntities context = GetDataBaseContext();
      List<Person> persons = context.Persons!
        .Where(p => p.Id == id)
        .Include(p => p.Versions).ToList();

      if (persons.Count <= 0) return false;
      context.Persons!.RemoveRange(persons);
      context.SaveChanges();
      return true;
    }

    public string Gender(DGender gender)
    {
      return gender switch
      {
        DGender.Female => "Female",
        DGender.Male => "Male",
        _ => "Other"
      };
    }
  }
}
