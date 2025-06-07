using System.ComponentModel.DataAnnotations;

namespace ConsoleFrontEnd.Models;

public class Locations
{
    [Key]
    public int LocationId { get; set; }
    public string Name { get; set; } 
    public string Address { get; set; } 
    public string TownOrCity { get; set; } 
    public string StateOrCounty { get; set; } 
    public string ZipOrPostCode { get; set; } 
    public string Country { get; set; } 

    public virtual ICollection<Shifts> Shifts { get; set; } // Navigation property to the Shifts entity
    public virtual ICollection<Workers> Workers { get; set; } // Navigation property to the Workers entity
}
