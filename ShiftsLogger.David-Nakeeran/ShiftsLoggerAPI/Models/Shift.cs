using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShiftsLoggerAPI.Models;

public class Shift
{
    [Key]
    public long ShiftId { get; set; }

    public long EmployeeId { get; set; }

    [Required]
    public DateTime StartTime { get; set; }

    [Required]
    public DateTime EndTime { get; set; }

    [ForeignKey(nameof(EmployeeId))]
    public Employee Employee { get; set; }
}