using System;

namespace ShiftLogger.Console_App.Models;

public class Shift
{
    public int Id { get; set; }
    public string EmployeeName { get; set; } = null!;
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public TimeSpan Duration => End - Start; 
}
