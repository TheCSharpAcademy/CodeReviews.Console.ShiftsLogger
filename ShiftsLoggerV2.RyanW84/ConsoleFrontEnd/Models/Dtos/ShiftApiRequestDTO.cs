using System.ComponentModel.DataAnnotations;

namespace ConsoleFrontEnd.Models.Dtos;

public class ShiftApiRequestDto
{
	[Required]
	[Range (1,255)]
	public int WorkerId { get; set; }
	[Required]
	public DateTimeOffset StartTime { get; set; }
	[Required]
	public DateTimeOffset EndTime { get; set; }
	[Required]
	[Range(1,255)]
	public int LocationId { get; set; }
	}
