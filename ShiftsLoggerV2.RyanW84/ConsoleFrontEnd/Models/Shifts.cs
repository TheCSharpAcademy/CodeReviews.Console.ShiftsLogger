using System.ComponentModel.DataAnnotations;

namespace ConsoleFrontEnd.Models;


public class Shifts
{
    [Key]
    public int ShiftId { get; set; }
    public int WorkerId { get; set; }
    public int LocationId { get; set; }
    public DateTimeOffset StartTime { get; set; }
    public DateTimeOffset EndTime { get; set; }

    // Navigation property to the Location entity
    public virtual Locations Location { get; set; }

    // Navigation property to the Worker entity
    public virtual Workers Worker { get; set; }
}
