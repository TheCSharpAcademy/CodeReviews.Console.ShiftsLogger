namespace API.Models;

public class Shift
{
    public int Id { get; set; }
    public string Employee { get; set; } = string.Empty;
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public TimeSpan Duration { get; set; }
}
