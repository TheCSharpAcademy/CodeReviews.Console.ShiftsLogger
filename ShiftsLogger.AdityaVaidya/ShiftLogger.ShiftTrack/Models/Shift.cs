namespace ShiftLogger.ShiftTrack.Models;

internal class Shift
{
    public string Date { get; set; } = null!;
    public string StartTime { get; set; } = null!;
    public string EndTime { get; set; } = null!;
    public string Duration { get; set; } = null!;
    public int WorkerId { get; set; }
}