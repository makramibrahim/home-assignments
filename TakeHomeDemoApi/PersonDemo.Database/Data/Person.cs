using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DatabaseEntities.Enums;

namespace DatabaseEntities.Data
{
    [Table("Persons")]
  public class Person
  {
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string? GivenName { get; set; }

    [MaxLength(255)]
    public string? Surname { get; set; }

    public Gender Gender { get; set; }

    [Required]
    public DateTime? BirthDate { get; set; }
    [MaxLength(255)]
    public string? BirthLocation { get; set; }

    public DateTime? DeathDate { get; set; }

    [MaxLength(255)]
    public string? DeathLocation { get; set; }

    // Navigation property to access the person's version history
    public virtual ICollection<PersonVersion>? Versions { get; set; }
  }
}
