using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShiftsLoggerV2.RyanW84.Models;

public class Shift
{
[Key]
    public int ShiftId { get; set; }
  
	public int WorkerId { get; set; }
    public DateTimeOffset StartTime { get; set; }
    public DateTimeOffset EndTime { get; set; }
    public int LocationId { get; set; }

	public virtual Location? Location { get; set; } = null!; // Navigation property to the Location entity
}
