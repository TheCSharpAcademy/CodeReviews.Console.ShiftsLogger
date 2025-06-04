using System.ComponentModel.DataAnnotations;

namespace ShiftsLogger.WebApi.RestModels;

public class UserUpdate
{
	[Display(Name = "First name")]
	[StringLength(100, ErrorMessage = "First name should be within 100 characters!")]
	public string? FirstName { get; set; }

	[Display(Name = "Last name")]
	[StringLength(100, ErrorMessage = "Last name should be within 100 characters!")]
	public string? LastName { get; set; }

	[Display(Name = "User email")]
	[StringLength(100, ErrorMessage = "User email should be within 100 characters!")]
	[EmailAddress(ErrorMessage = "Invalid email adress!")]
	public string? Email { get; set; }
}
