using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ShiftsLoggerWebAPI.Models;
[Index(nameof(Name), IsUnique = true)]
public class Employee
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }

    public ICollection<Shift> Shifts { get; set; } = new List<Shift>();
}