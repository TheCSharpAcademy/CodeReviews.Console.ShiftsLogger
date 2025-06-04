namespace ShiftsLogger.ConsoleUI.Models;
public class User : IUser
{
	public Guid Id { get; set; }
	public string? FirstName { get; set; }
	public string? LastName { get; set; }
	public string? Email { get; set; }

	public override string ToString()
	{
		return string.Join(Environment.NewLine, $"{FirstName} {LastName}");
	}
}
