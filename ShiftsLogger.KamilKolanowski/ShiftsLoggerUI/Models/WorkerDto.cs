using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ShiftsLoggerUI.Models;

public class WorkerDto
{
    public int WorkerId { get; set; }
    public string FirstName { get; set; } = String.Empty;
    public string LastName { get; set; } = String.Empty;
    public string Email { get; set; } = String.Empty;
    public string Role { get; set; } = String.Empty;
}
