using DatabaseEntities.Services;
using PersonDemo.API.Models;

namespace PersonDemo.API.Services.interfaces
{
  public interface IPersonService : IErrorMessage
  {
    bool CreatePerson(DPerson? person);
    bool UpdatePerson(DPerson person);
    DPerson GetPerson(Guid id);
    List<DPerson> GetAll();
    bool Delete(Guid id);
  }
}
