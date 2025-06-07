using System.ComponentModel.DataAnnotations;

namespace ConsoleFrontEnd.Models;

public class Workers
{
    [Key]
    public int WorkerId { get; set; }
    public string Name { get; set; } 

    [Phone]
    public string? PhoneNumber { get; set; }

    [EmailAddress]
    public string? Email { get; set; }

    public virtual ICollection<Shifts?> Shifts { get; set; }
    public virtual ICollection<Locations?> Locations { get; set; }
}
