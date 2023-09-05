using PersonDemo.API.Models.Enums;

namespace PersonDemo.API.Models
{
  public class DPerson
  {
    public Guid Id { get; set; }
    public string? GivenName { get; set; }
    public string? Surname { get; set; }
    public DGender Gender { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? BirthLocation { get; set; }
    public DateTime? DeathDate { get; set; }
    public string? DeathLocation { get; set; }
    public List<DPersonVersion> Versions { get; set; } = new ();
  }
}
