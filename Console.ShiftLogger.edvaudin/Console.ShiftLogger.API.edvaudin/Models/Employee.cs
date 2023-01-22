using System.ComponentModel.DataAnnotations;

namespace ShiftLogger.API.Models;

public class Employee
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    public List<Shift>? Shifts { get; set; }
}
