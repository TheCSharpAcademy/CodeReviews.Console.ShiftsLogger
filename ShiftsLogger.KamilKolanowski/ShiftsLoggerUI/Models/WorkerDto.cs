using System.ComponentModel.DataAnnotations;

namespace ShiftsLogger.KamilKolanowski.Models;

public class WorkerDto
{
    [Key]
    public int WorkerId { get; set; }
    
    public string FirstName { get; set; } = String.Empty;
    public string LastName { get; set; } = String.Empty;
    public string Email { get; set; } = String.Empty;
    public string Role { get; set; } = String.Empty;
    
    public List<ShiftDto> Shifts { get; set; } 
}
