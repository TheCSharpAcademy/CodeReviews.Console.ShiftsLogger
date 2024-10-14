using System.ComponentModel.DataAnnotations;

namespace ShiftLogger.Api.Models;

public class Shift
{
    [Key]
    public int Id { get; set; }
    public string EmployeeName {get; set;} = null!;
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public TimeSpan Duration => End - Start;
}
