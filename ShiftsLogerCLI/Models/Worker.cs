using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace ShiftsLogerCLI.Models;

public class Worker
{
    [Key]
    public int Id { get; set; }
    [Required,MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    [Required,MaxLength(100)]
    public string Email { get; set; } = string.Empty;
    [Required,MaxLength(100)]
    public string Position { get; set; } = string.Empty;
    [Required]
    public DateTime HireDate { get; set; }
    [JsonIgnore] // This will exclude WorkerShifts from being serialized in the JSON response
    public ICollection<Shift> Shifts { get; set; } = new List<Shift>();
    public Worker() { }
    public Worker(int id,string name, string email,string position,DateTime hireDate)
    {
        Id = id;
        Name = name;
        Email = email;
        Position = position;
        HireDate = hireDate;
    }
    public Worker(WorkerCreate worker)
    {
        Name = worker.Name;
        Email = worker.Email;
        Position = worker.Position;
        HireDate = worker.HireDate;
    }
    
}
public record WorkerCreate(string Name,string Email,string Position,DateTime HireDate);
public  record WorkerUpdate(int Id,string? Name,string? Email,string? Position,DateTime? HireDate);