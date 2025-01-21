using Microsoft.AspNetCore.Mvc;

namespace ShiftsLogger.Bina28.Dtos;

public class FilterOptions
{
	[FromQuery(Name = "page_size")]
	public int PageSize { get; set; } = 10;

	[FromQuery(Name = "page_number")]
	public int PageNumber { get; set; } = 1;

}
