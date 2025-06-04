using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ShiftsLogger.Common.Validation;

namespace ShiftsLogger.WebApi.RestModels;

public class ShiftCreate
{
	[JsonRequired]
	[Display(Name = "Shift start date and time")]
	public DateTime StartTime { get; set; }

	[JsonRequired]
	[Display(Name = "Shift end date and time")]
	[DateGreaterThan("StartTime")]
	public DateTime EndTime { get; set; }
}
