using System.ComponentModel.DataAnnotations;

namespace ShiftsLogger.WebApi.RestModels;

public class UserRead
{
	[Display(Name = "User Id")]
	public Guid Id { get; set; }
	[Display(Name = "First name")]
	public string? FirstName { get; set; }
	[Display(Name = "Last name")]
	public string? LastName { get; set; }
	[Display(Name = "User email")]
	public string? Email { get; set; }
}
