namespace ShiftsLogger.ConsoleUI.Models;
public class Shift : IShift
{
	public Guid Id { get; set; }
	public DateTime StartTime { get; set; }
	public DateTime EndTime { get; set; }
	public double GetDuration()
	{
		return (EndTime - StartTime).TotalHours;
	}
	public override string ToString()
	{
		return $"{StartTime.ToShortDateString()} {StartTime.ToShortTimeString()}-{EndTime.ToShortTimeString()}";
	}
}
