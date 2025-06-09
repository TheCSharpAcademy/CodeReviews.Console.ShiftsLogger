using System.Net.Http.Json;

using ConsoleFrontEnd.Models;
using ConsoleFrontEnd.Models.Dtos;
using ConsoleFrontEnd.Models.FilterOptions;

using Spectre.Console;

namespace ConsoleFrontEnd.Services;

public class LocationService : ILocationService
{
	private readonly HttpClient httpClient = new HttpClient()
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
			var queryString =
				$"api/locations?locationId={locationFilterOptions.LocationId}&locationName={locationFilterOptions.Name}&locationAddress{locationFilterOptions.Address}&locationTownOrCity{locationFilterOptions.TownOrCity} &locationStateOrCounty{locationFilterOptions.StateOrCounty} &locationZipOrPostCode{locationFilterOptions.ZipOrPostCode} "}
			;

			response = await httpClient.GetAsync(queryString);
			if (!response.IsSuccessStatusCode)
			{
				Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
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
					Data = new List<Locations>() ,
					TotalCount = 0 ,
				};
			}
			else
			{
				Console.WriteLine("Locations retrieved successfully.");
				ApiResponseDto<List<Locations>>? createdLocation =
					await response.Content.ReadFromJsonAsync<ApiResponseDto<List<Locations>>>()
					?? new ApiResponseDto<List<Locations>>
					{
						ResponseCode = response.StatusCode ,
						Message = "No data returned." ,
						Data = new List<Locations>() ,
						TotalCount = 0 ,
					};

				return createdLocation;
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Try catch failed for GetAllLocations: {ex}");
			throw;
		}
	}

	public async Task<ApiResponseDto<List<Locations?>>> GetLocationById(int id)
	{
		HttpResponseMessage response;
		try
		{
			response = await httpClient.GetAsync($"api/locations/{id}");

			if (response.StatusCode is not System.Net.HttpStatusCode.OK)
			{
				AnsiConsole.Markup($"[Red]Error: Location not found[/]\n");
				return new ApiResponseDto<List<Locations>>
				{
					ResponseCode = response.StatusCode ,
					Message = response.ReasonPhrase ,
					Data = null ,
				};
			}
			else
			{
				AnsiConsole.Markup("[Green]Location retrieved successfully.[/]\n");
				return await response.Content.ReadFromJsonAsync<ApiResponseDto<List<Locations>>>()
					?? new ApiResponseDto<List<Locations>>
					{
						ResponseCode = response.StatusCode ,
						Message = "Location found" ,
						Data = new List<Locations>() ,
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
			if (response.StatusCode is not System.Net.HttpStatusCode.Created)
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
					Data = response.Content.ReadFromJsonAsync<Locations>().Result ?? createdLocation ,
				};
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Try catch failed for CreateShift: {ex}");
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
				AnsiConsole.Markup("[Green]Location updated successfully.[/]\n");
				Console.WriteLine("Press any key to continue");
				Console.ReadKey();
				Console.Clear();
				return new ApiResponseDto<Locations>
				{
					ResponseCode = response.StatusCode ,
					Data = response.Content.ReadFromJsonAsync<Locations>().Result ?? updatedLocation ,
				};
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Try catch failed for UpdateLocation: {ex}");
			throw;
		}
	}

	public async Task<ApiResponseDto<string?>> DeleteLocation(int id)
	{
		HttpResponseMessage response;
		try
		{
			response = await httpClient.DeleteAsync($"api/locations/{id}");
			if (response.StatusCode is not System.Net.HttpStatusCode.NoContent)
			{
				AnsiConsole.Markup("[red]Error: Location not found please try again![/]\n");
				return new ApiResponseDto<string>
				{
					ResponseCode = response.StatusCode ,
					Message = $"Error: {response.StatusCode}" ,
					Data = null ,
				};
			}
			else
			{
				AnsiConsole.Markup("[green]Location deleted successfully![/]");
				return new ApiResponseDto<string>
				{
					ResponseCode = response.StatusCode ,
					Message = response.ReasonPhrase ,
					Data = null ,
				};
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Try catch failed for DeleteLocation: {ex}");
			throw;
		}
	}
}
