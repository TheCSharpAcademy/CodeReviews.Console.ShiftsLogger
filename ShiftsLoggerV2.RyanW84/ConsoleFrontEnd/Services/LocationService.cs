using System.Net.Http.Json;

using ConsoleFrontEnd.Models;
using ConsoleFrontEnd.Models.Dtos;
using ConsoleFrontEnd.Models.FilterOptions;

using Spectre.Console;

namespace ConsoleFrontEnd.Services;

public class LocationService : ILocationService
{
	private readonly HttpClient httpClient = new ()
	{
		BaseAddress = new Uri("https://localhost:7009/") ,
	};

	public async Task<ApiResponseDto<List<Locations>>> GetAllLocations(
		LocationFilterOptions locationFilterOptions
	)
	{
		HttpResponseMessage response;
		try
		{
			var queryParams = new List<string>();
			if (locationFilterOptions.LocationId != null)
				queryParams.Add($"LocationId={locationFilterOptions.LocationId}");
			if (locationFilterOptions.Name != null)
				queryParams.Add($"Name={locationFilterOptions.Name}");
			if (locationFilterOptions.LocationId != null)
				queryParams.Add($"Address={locationFilterOptions.Address}");
			if (locationFilterOptions.TownOrCity != null)
				queryParams.Add($"TownOrCity={locationFilterOptions.TownOrCity:O}");
			if (locationFilterOptions.StateOrCounty != null)
				queryParams.Add($"endTime={locationFilterOptions.StateOrCounty:O}");
			if (locationFilterOptions.ZipOrPostCode != null)
				queryParams.Add($"ZipOrPostCode={locationFilterOptions.ZipOrPostCode:O}");
			if (locationFilterOptions.Country != null)
				queryParams.Add($"Country={locationFilterOptions.Country:O}");

			var queryString = "api/locations";
			if (queryParams.Count > 0)
				queryString += "?" + string.Join("&" , queryParams);

			response = await httpClient.GetAsync(queryString);
			if (!response.IsSuccessStatusCode)
			{
				return new ApiResponseDto<List<Locations>>
				{
					ResponseCode = response.StatusCode ,
					Message = response.ReasonPhrase ,
					Data = null ,
				};
			}
			else if (response.StatusCode is System.Net.HttpStatusCode.NoContent)
			{
				Console.WriteLine("No locations found.");
				return new ApiResponseDto<List<Locations>>
				{
					ResponseCode = response.StatusCode ,
					Message = "No locations found." ,
					Data = [],
					TotalCount = 0
				};
			}
			else
			{
				var locations =
					await response.Content.ReadFromJsonAsync<ApiResponseDto<List<Locations>>>()
					?? new ApiResponseDto<List<Locations>>
					{
						ResponseCode = response.StatusCode ,
						Message = "Data obtained" ,
						Data = []
					};


				return locations;
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Try catch failed for GetAllLocations: {ex}");
			throw;
		}
	}

	public async Task<ApiResponseDto<List<Locations>>> GetLocationById(int id)
	{
		HttpResponseMessage response;
		try
		{
			response = await httpClient.GetAsync($"api/locations/{id}");

			if (response.StatusCode is not System.Net.HttpStatusCode.OK)
			{
				AnsiConsole.Markup($"\n[Red]Error - {response.StatusCode}[/]\n");

				return new ApiResponseDto<List<Locations>>
				{
					ResponseCode = response.StatusCode ,
					Message = response.ReasonPhrase ,
					Data = null ,
				};
			}
			else
			{
				AnsiConsole.Markup("\n[Green]Location retrieved successfully[/]\n");
				return await response.Content.ReadFromJsonAsync<ApiResponseDto<List<Locations>>>()
					?? new ApiResponseDto<List<Locations>>
					{
						ResponseCode = response.StatusCode ,
						Message = "No data returned." ,
						Data = [],
						TotalCount = 0 ,
					};
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Try catch failed for GetLocationById: {ex}");
			throw;
		}
	}

	public async Task<ApiResponseDto<Locations>> CreateLocation(Locations createdLocation)
	{
		HttpResponseMessage response;
		try
		{
			response = await httpClient.PostAsJsonAsync("api/locations" , createdLocation);
			if (
				response.StatusCode is not System.Net.HttpStatusCode.OK
				|| response.StatusCode is not System.Net.HttpStatusCode.Created
			)
			{
				Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
				return new ApiResponseDto<Locations>
				{
					ResponseCode = response.StatusCode ,
					Message = response.ReasonPhrase ,
					Data = null ,
				};
			}
			else
			{
				Console.WriteLine("Location created successfully.");
				return new ApiResponseDto<Locations>
				{
					ResponseCode = response.StatusCode ,
					Data = createdLocation ,
				};
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Try catch failed for CreateLocation: {ex}");
			throw;
		}
	}

	public async Task<ApiResponseDto<Locations?>> UpdateLocation(int id , Locations updatedLocation)
	{
		HttpResponseMessage response;
		try
		{
			response = await httpClient.PutAsJsonAsync($"api/locations/{id}" , updatedLocation);
			if (response.StatusCode is not System.Net.HttpStatusCode.OK)
			{
				AnsiConsole.Markup($"\n[red]Error - {response.StatusCode}[/]\n");
				return new ApiResponseDto<Locations>
				{
					ResponseCode = response.StatusCode ,
					Message = response.ReasonPhrase ,
					Data = null ,
				};
			}
			else
			{
				AnsiConsole.Markup("\n[Green]Location retrieved successfully[/]\n");
				return await response.Content.ReadFromJsonAsync<ApiResponseDto<Locations>>()
					?? new ApiResponseDto<Locations>
					{
						ResponseCode = response.StatusCode ,
						Message = "Update Location succeeded." ,
						Data = null ,
					};
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Try catch failed for UpdateLocation: {ex}");
			throw;
		}
	}
}


//	public async Task<ApiResponseDto<string?>> DeleteLocation(int id)
//	{
//		HttpResponseMessage response;
//		try
//		{

//		}
//		catch (Exception)
//		{

//			throw;
//		}
//		{
//			response = await httpClient.DeleteAsync($"api/locations/{id}");
//			if (response.StatusCode is System.Net.HttpStatusCode.NotFound)
//			{
//				Console.WriteLine($"Error - {response.StatusCode}");
//				return new ApiResponseDto<string>
//				{
//					ResponseCode = response.StatusCode ,
//					Message = "Location not found." ,
//					Data = null ,
//				};
//			}
//			else if (response.StatusCode is System.Net.HttpStatusCode.NoContent)
//			{
//				AnsiConsole.Markup("\n[Green]Location deleted successfully![/]\n");
//				return new ApiResponseDto<string>
//				{
//					ResponseCode = response.StatusCode ,
//					Message = response.ReasonPhrase ,
//					Data = null ,
//				};
//			}
//			else
//			{
//				AnsiConsole.Markup($"[red]Error - {response.StatusCode}[/]");
//				return new ApiResponseDto<string>
//				{
//					ResponseCode = response.StatusCode ,
//					Message = response.ReasonPhrase ,
//					Data = null ,
//				};
//			}
//		}
//	}
//}
//		catch (HttpRequestException ex)
//		{
//			Console.WriteLine($"HTTP request failed for DeleteLocation: {ex.Message}");
//			throw;
//		}
	