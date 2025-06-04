using System.ComponentModel.DataAnnotations;

namespace ShiftsLogger.WebApi.RestModels;

public class ShiftRead
{
	[Display(Name = "Shift Id")]
	public Guid Id { get; set; }

	[Display(Name = "Shift start date and time")]
	public DateTime StartTime { get; set; }

	[Display(Name = "Shift end date and time")]
	public DateTime EndTime { get; set; }
}
