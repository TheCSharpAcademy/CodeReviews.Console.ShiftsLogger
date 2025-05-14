using System.ComponentModel.DataAnnotations;

namespace ShiftsLogger.KamilKolanowski.Models;

public class ShiftDto
{
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}
