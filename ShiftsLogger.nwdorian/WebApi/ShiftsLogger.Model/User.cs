namespace ShiftsLogger.Model;
public class User
{
	public Guid Id { get; set; }
	public string? FirstName { get; set; }
	public string? LastName { get; set; }
	public string? Email { get; set; }
	public bool IsActive { get; set; }
	public DateTime DateCreated { get; set; }
	public DateTime DateUpdated { get; set; }
}
