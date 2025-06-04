namespace ShiftsLogger.ConsoleUI.Models;
public class ShiftCreate(DateTime startTime, DateTime endTime) : IShift
{
	public DateTime StartTime { get; set; } = startTime;
	public DateTime EndTime { get; set; } = endTime;
}
