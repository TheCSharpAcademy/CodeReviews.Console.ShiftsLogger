using System.ComponentModel.DataAnnotations;

namespace ShiftsLoggerV2.RyanW84.Dtos;

public class WorkerApiRequestDto
{
	[Required]
	[Range(1, 255)]
	public int WorkerId { get; set; }
	[Required]
	[MinLength(1)]
	[MaxLength(255)]
	public string Name { get; set; } = string.Empty;
	[Phone]
	public string? Phone { get; set; }
	[EmailAddress]
	public string? Email { get; set; }
}
