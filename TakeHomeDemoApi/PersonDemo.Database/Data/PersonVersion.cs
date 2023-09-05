using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DatabaseEntities.Enums;

namespace DatabaseEntities.Data
{
    [Table("PersonVersions")]
  public class PersonVersion
  {
    [Key]
    public int Id { get; set; }

    [Required] 
    public Guid PersonId { get; set; }

    [Required]
    [MaxLength(255)] 
    public string? GivenName { get; set; }

    [MaxLength(255)] 
    public string? Surname { get; set; }

    public Gender Gender { get; set; }

    public DateTime? BirthDate { get; set; }

    [MaxLength(255)] 
    public string? BirthLocation { get; set; }

    public DateTime? DeathDate { get; set; }

    [MaxLength(255)] 
    public string? DeathLocation { get; set; }

    [Required] 
    public DateTime Timestamp { get; set; }

  }
}
