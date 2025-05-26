using System.ComponentModel.DataAnnotations;

namespace ConsoleFrontEnd.Models;

public class Locations
{
    [Key]
    public int LocationId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string TownOrCity { get; set; } = string.Empty;
    public string StateOrCounty { get; set; } = string.Empty;
    public string ZipOrPostCode { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;

    public virtual ICollection<Shifts> Shifts { get; set; } // Navigation property to the Shifts entity
    public virtual ICollection<Workers> Workers { get; set; } // Navigation property to the Workers entity
}
