using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseEntities.Data
{
  [Table("ErrorLog")]
  public class ErrorLog
  {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public DateTime? CreatedDate { get; set; }
    public string? HttpMethod { get; set; }
    public string? RequestURI { get; set; }
    public string? RequestHeaders { get; set; }
    public string? RequestJSON { get; set; }
    public string? ErrorMessage { get; set; }
    public string? Referer { get; set; }
    public string? Origin { get; set; }
    public int? SystemAccessId { get; set; }
  }
}