using System.ComponentModel.DataAnnotations;

namespace ShiftsLoggerAPI.Models;

public class Employee
{
    [Key]
    public long EmployeeId { get; set; }

    [Required]
    public required string Name { get; set; }

    public List<Shift> Shifts { get; set; }

}