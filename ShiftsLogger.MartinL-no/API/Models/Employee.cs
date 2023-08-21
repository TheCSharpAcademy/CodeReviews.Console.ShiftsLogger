using System.ComponentModel.DataAnnotations;

namespace API.Models;

public class Employee
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public ICollection<Shift> Shifts { get; } = new List<Shift>();
}
