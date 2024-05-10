namespace shiftloggerconsole.Models;

internal class Shift
{
    public int Id { get; set; }
    public int WorkerId { get; set; }
    public DateTime ClockIn { get; set; }
    public DateTime ClockOut { get; set; }
    public string? Duration { get; set; }
}
