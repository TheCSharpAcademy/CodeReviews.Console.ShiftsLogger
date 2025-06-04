namespace ShiftsLogger.DAL.Entities;
public class UserEntity
{
	public Guid Id { get; set; }
	public string? FirstName { get; set; }
	public string? LastName { get; set; }
	public string? Email { get; set; }
	public bool IsActive { get; set; }
	public DateTime DateCreated { get; set; }
	public DateTime DateUpdated { get; set; }
	public List<ShiftEntity> Shifts { get; protected set; } = new List<ShiftEntity>();
}
