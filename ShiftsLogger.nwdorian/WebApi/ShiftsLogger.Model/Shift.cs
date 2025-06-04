namespace ShiftsLogger.Model;
public class Shift
{
	public Guid Id { get; set; }
	public DateTime StartTime { get; set; }
	public DateTime EndTime { get; set; }
	public bool IsActive { get; set; }
	public DateTime DateCreated { get; set; }
	public DateTime DateUpdated { get; set; }
}
