using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShiftsLoggerV2.RyanW84.Models;

public class Workers
{
    [Key]
    public int WorkerId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }

    public virtual ICollection<Shifts?> Shifts { get; set; } // Navigation property to the Shifts entity
    public virtual ICollection<Locations?> Locations { get; set; }
}
