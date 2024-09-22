using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShiftsLogger.AdityaVaidya.Models;

public class Shift
{
    [Key]
    public int ShiftId { get; set; }
    public string Date { get; set; } = null!;
    public string StartTime { get; set; } = null!;
    public string EndTime { get; set; } = null!;
    public string Duration { get; set; } = null!;
    public int WorkerId { get; set; }
    [ForeignKey("WorkerId")]
    public Worker Worker { get; set; } = null!;
}
