using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShiftsLoggerV2.RyanW84.Models;

public class Worker
{
    [Key]
    public int WorkerId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? Email { get; set; }

    public virtual ICollection<Shift?> Shifts { get; set; } // Navigation property to the Shifts entity
}
