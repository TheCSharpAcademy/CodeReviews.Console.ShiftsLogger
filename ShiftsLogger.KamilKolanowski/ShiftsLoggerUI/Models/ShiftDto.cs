using System.ComponentModel.DataAnnotations;

namespace ShiftsLogger.KamilKolanowski.Models;

public class ShiftDto
{
    [Key]
    public int ShiftId { get; set; }

    [Required]
    public int ShiftTypeId { get; set; }
    public ShiftTypeDto ShiftTypeDto { get; set; }

    public double WorkedHours { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public List<WorkerDto> Workers { get; set; }
}
