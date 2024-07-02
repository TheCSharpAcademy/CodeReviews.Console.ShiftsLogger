using System.ComponentModel.DataAnnotations;

namespace ShiftsLogger.K_MYR;

public class ShiftInsertModel
{
    [Required]
    public DateTime StartTime { get; set; }
    [Required]
    public DateTime EndTime { get; set; }
}
