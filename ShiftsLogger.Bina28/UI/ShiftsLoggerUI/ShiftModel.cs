namespace ShiftsLoggerUI;

internal class ShiftModel
{
	public int Id { get; set; }
	public int EmployeeId { get; set; }
	public DateTime StartTime { get; set; }
	public DateTime EndTime { get; set; }
	public string ShiftType { get; set; } = string.Empty;
	public string Notes { get; set; } = string.Empty;
	public TimeSpan Duration => EndTime - StartTime;

}
