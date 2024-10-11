using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShiftsLogger.API.Data
{
  public class Shift
  {
    [Key]
    public int Id { get; set; }
    public int WorkerId { get; set; }

    [ForeignKey(nameof(WorkerId))]
    public required Worker Worker { get; set; }

    public DateTime Start { get; set; }

    public DateTime End { get; set; }
  }
}
