using System.ComponentModel.DataAnnotations;

namespace ShiftsLoggerAPI.Models;

public class Worker
{
    [Key]
    public int WorkerId { get; set; }
    public string FirstName { get; set; } = String.Empty;
    public string LastName { get; set; } = String.Empty;
    public string Email { get; set; } = String.Empty;
    public string Role { get; set; } = String.Empty;
}
