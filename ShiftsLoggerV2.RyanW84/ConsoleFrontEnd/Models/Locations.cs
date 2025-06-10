using System.ComponentModel.DataAnnotations;

namespace ConsoleFrontEnd.Models;

public class Locations
{
    [Key]
    public int LocationId { get; set; }
    public required string Name { get; set; }
    public required string Address { get; set; }
    public required string TownOrCity { get; set; }
    public required string StateOrCounty { get; set; }
    public required string ZipOrPostCode { get; set; }
    public required string Country { get; set; }

    public virtual ICollection<Shifts> Shifts { get; set; } // Navigation property to the Shifts entity
    public virtual ICollection<Workers> Workers { get; set; } // Navigation property to the Workers entity
}
