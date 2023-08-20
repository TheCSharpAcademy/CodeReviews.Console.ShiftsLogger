using System.ComponentModel.DataAnnotations;

namespace API.Models;

internal class Shift
{
    public int Id { get; set; }
    [Required]
    public DateTime StartTime { get; set; }
    [Required]
    public DateTime EndTime { get; set; }
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; } = null!;
}