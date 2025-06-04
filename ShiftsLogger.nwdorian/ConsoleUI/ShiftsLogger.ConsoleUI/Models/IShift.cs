namespace ShiftsLogger.ConsoleUI.Models;
public interface IShift
{
	DateTime StartTime { get; set; }
	DateTime EndTime { get; set; }
}
