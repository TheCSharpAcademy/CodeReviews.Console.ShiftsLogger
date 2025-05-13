using System.ComponentModel.DataAnnotations;

namespace ShiftsLoggerV2.RyanW84.Models;

public class Location
{
    [Key]
    public int LocationId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string TownOrCity { get; set; } = string.Empty;
    public string StateOrCounty { get; set; } = string.Empty;
    public string ZipOrPostCode { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;

    public virtual Shift? Shift { get; set; } = null!; // Navigation property to the Shift entity
}
