using ShiftsLogger.API.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class Shift
{
  [Key]
  public int Id { get; set; }
  public int WorkerId { get; set; }
  [ForeignKey(nameof(WorkerId))]
  [JsonIgnore]
  public Worker? Worker { get; set; }  // Make nullable
  public DateTime Start { get; set; }
  public DateTime End { get; set; }
}