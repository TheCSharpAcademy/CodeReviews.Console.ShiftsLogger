using System.ComponentModel.DataAnnotations.Schema;

namespace ShiftsLogger.Dejmenek.API.Models;

public class Shift
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    [Column(TypeName = "DateTime")]
    public DateTime StartTime { get; set; }
    [Column(TypeName = "DateTime")]
    public DateTime EndTime { get; set; }
    public string Duration { get; set; } = null!;
    public Employee Employee { get; set; } = null!;
}

public class ShiftCreateDTO
{
    public int EmployeeId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string Duration { get; set; } = null!;
}

public class ShiftUpdateDTO
{
    public DateTime EndTime { get; set; }
    public string Duration { get; set; } = null!;
}

public class ShiftReadDTO
{
    public int Id { get; set; }
    public string EmployeeFirstName { get; set; } = null!;
    public string EmployeeLastName { get; set; } = null!;
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string Duration { get; set; } = null!;
}
