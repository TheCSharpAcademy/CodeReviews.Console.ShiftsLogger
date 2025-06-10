namespace ConsoleFrontEnd.Models.Dtos;

public class LocationsDto
{
	public int LocationId { get; set; }
	public string Name { get; set; } = string.Empty;
	public string Address { get; set; } = string.Empty;
	public string TownOrCity { get; set; } = string.Empty;
	public string StateOrCounty { get; set; } = string.Empty;
	public string ZipOrPostCode { get; set; } = string.Empty;
	public string Country { get; set; } = string.Empty;
}
