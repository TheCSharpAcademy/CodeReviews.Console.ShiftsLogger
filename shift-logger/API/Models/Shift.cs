namespace API.Models;

public class Shift
{
    public int Id { get; set; }
    public string EmployeeName { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public TimeSpan Duration { get; set; }
}