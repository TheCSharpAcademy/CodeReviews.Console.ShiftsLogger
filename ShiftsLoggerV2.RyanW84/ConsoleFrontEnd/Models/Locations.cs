using System.ComponentModel.DataAnnotations;

namespace ConsoleFrontEnd.Models;

public class Locations
{
    [Key]
    public int locationId { get; set; }
    public required string name { get; set; }
    public required string address { get; set; }
    public required string townOrCity { get; set; }
    public required string stateOrCounty { get; set; }
    public required string zipOrPostCode { get; set; }
    public required string country { get; set; }

    public virtual ICollection<Shifts> Shifts { get; set; } // Navigation property to the Shifts entity
    public virtual ICollection<Workers> Workers { get; set; } // Navigation property to the Workers entity
}
