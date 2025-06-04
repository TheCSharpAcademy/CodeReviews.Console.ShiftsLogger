namespace ShiftsLogger.ConsoleUI.Models;
public class UserUpdate(string firstName, string lastName, string email) : IUser
{
	public string? FirstName { get; set; } = firstName;
	public string? LastName { get; set; } = lastName;
	public string? Email { get; set; } = email;

	public override string ToString()
	{
		return string.Join(Environment.NewLine, $"{FirstName} {LastName}");
	}
}
