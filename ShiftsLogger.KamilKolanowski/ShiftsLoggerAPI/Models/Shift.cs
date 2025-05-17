using System.ComponentModel.DataAnnotations;

namespace ShiftsLogger.KamilKolanowski.Models;

public class Shift
{
    [Key]
    public int ShiftId { get; set; }

    [Required]
    public string ShiftType { get; set; }

    [Required]
    public int WorkerId { get; set; }

    public string WorkedHours { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}
