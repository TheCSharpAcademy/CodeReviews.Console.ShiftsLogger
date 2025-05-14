using System.ComponentModel.DataAnnotations;

namespace ShiftsLogger.KamilKolanowski.Models;

public class Shift
{
    [Key]
    public int ShiftId { get; set; }
    [Required]
    public int ShiftTypeId { get; set; }
    [Required]
    public int WorkerId { get; set; }
    public ShiftType ShiftType { get; set; }
    public double WorkedHours { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

}
