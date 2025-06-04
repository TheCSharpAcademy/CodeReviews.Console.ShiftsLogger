namespace ShiftsLogger.ConsoleUI.Models;
public class ShiftUpdate(DateTime startTime, DateTime endTime) : IShift
{
	public DateTime StartTime { get; set; } = startTime;
	public DateTime EndTime { get; set; } = endTime;

	public double GetDuration()
	{
		return (EndTime - StartTime).TotalHours;
	}
}
