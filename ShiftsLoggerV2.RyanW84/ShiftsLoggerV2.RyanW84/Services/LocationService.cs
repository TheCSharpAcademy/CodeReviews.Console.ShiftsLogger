using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShiftsLoggerV2.RyanW84.Data;
using ShiftsLoggerV2.RyanW84.Dtos;
using ShiftsLoggerV2.RyanW84.Models;
using ShiftsLoggerV2.RyanW84.Models.FilterOptions;
using Spectre.Console;

namespace ShiftsLoggerV2.RyanW84.Services;

public class LocationService(Data.ShiftsLoggerDbContext dbContext, IMapper mapper) : ILocationService
{
    public async Task<ApiResponseDto<List<Locations>>> GetAllLocations(
        LocationFilterOptions locationOptions
    )
    {
        if (locationOptions == null)
        {
            return new ApiResponseDto<List<Locations>>
            {
                RequestFailed = true,
                ResponseCode = System.Net.HttpStatusCode.BadRequest,
                Message = "Location filter options cannot be null.",
            };
        }

        var query = dbContext
            .Locations.Include(l => l.Shifts)
            .Include(l => l.Workers)
            .AsQueryable();

        List<Locations>? locations;

        if (locationOptions.LocationId != 0)
        {
            query = query.Where(l => l.LocationId == locationOptions.LocationId);
        }

        if (!string.IsNullOrEmpty(locationOptions.SortBy) && locationOptions.SortBy != "None")
        {
            if (!string.IsNullOrEmpty(locationOptions.SortBy))
            {
                var sortBy = locationOptions.SortBy.ToLowerInvariant();
                var sortOrder = locationOptions.SortOrder?.ToLowerInvariant() ?? "asc";

                switch (sortBy)
                {
                    case "location_id":
                        query = locationOptions.SortOrder.Equals(
                            "ASC",
                            StringComparison.CurrentCultureIgnoreCase
                        )
                            ? query.OrderBy(l => l.LocationId)
                            : query.OrderByDescending(l => l.LocationId);
                        break;
                    default:
                        query = locationOptions.SortOrder.Equals(
                            "ASC",
                            StringComparison.CurrentCultureIgnoreCase
                        )
                            ? query.OrderBy(l => l.LocationId)
                            : query.OrderByDescending(l => l.LocationId);
                        break;
                }
            }
        }

        if (!string.IsNullOrEmpty(locationOptions.Search))
        {
            string search = locationOptions.Search;
            var data = await query.ToListAsync();

            locations = data.Where(l =>
                    l.LocationId.ToString()
                        .Contains(search, StringComparison.CurrentCultureIgnoreCase)
                    || l.Name.ToLower().Contains(search, StringComparison.CurrentCultureIgnoreCase)
                    || l.Address.ToLower()
                        .Contains(search, StringComparison.CurrentCultureIgnoreCase)
                    || l.TownOrCity.Contains(search, StringComparison.CurrentCultureIgnoreCase)
                    || l.StateOrCounty.ToLower()
                        .Contains(search, StringComparison.CurrentCultureIgnoreCase)
                    || l.ZipOrPostCode.ToLower()
                        .Contains(search, StringComparison.CurrentCultureIgnoreCase)
                    || l.Country.ToLower()
                        .Contains(search, StringComparison.CurrentCultureIgnoreCase)
                )
                .ToList();
        }
        else
        {
            locations = await query.ToListAsync();
        }

        if (locations is null || locations.Count == 0)
        {
            return new ApiResponseDto<List<Locations>>
            {
                RequestFailed = true,
                ResponseCode = System.Net.HttpStatusCode.NotFound,
                Message = "No locations found.",
            };
        }

        return new ApiResponseDto<List<Locations>>
        {
            Data = locations,
            Message = "Locations retrieved successfully",
            ResponseCode = System.Net.HttpStatusCode.OK,
        };
    }

    public async Task<ApiResponseDto<Locations?>> GetLocationById(int id)
    {
        Locations? location = await dbContext
            .Locations.Include(l => l.Shifts)
            .FirstOrDefaultAsync(l => l.LocationId == id);
        if (location is null)
        {
            return new ApiResponseDto<Locations?>
            {
                RequestFailed = true,
                ResponseCode = System.Net.HttpStatusCode.NotFound,
                Message = $"Location with ID: {id} not found.",
            };
        }
        return new ApiResponseDto<Locations?>
        {
            Data = location,
            Message = "Location retrieved successfully",
            ResponseCode = System.Net.HttpStatusCode.OK,
        };
    }

    public async Task<ApiResponseDto<Locations>> CreateLocation(LocationApiRequestDto locationDto)
    {
        Locations newLocation = mapper.Map<Locations>(locationDto);
        var savedLocation = await dbContext.Locations.AddAsync(newLocation);
        await dbContext.SaveChangesAsync();

        AnsiConsole.MarkupLine(
            $"\n[green]Successfully created location with ID: {savedLocation.Entity.LocationId}[/]"
        );
        return new ApiResponseDto<Locations>
        {
            Data = savedLocation.Entity,
            Message = "Location created successfully",
            ResponseCode = System.Net.HttpStatusCode.Created,
        };
    }

    public async Task<ApiResponseDto<Locations?>> UpdateLocation(
        int id,
        LocationApiRequestDto updatedLocation
    )
    {
        Locations? savedLocation = await dbContext.Locations.FindAsync(id);

        if (savedLocation is null)
        {
            return new ApiResponseDto<Locations?>
            {
                RequestFailed = true,
                ResponseCode = System.Net.HttpStatusCode.NotFound,
                Message = $"Location with ID: {id} not found.",
            };
        }

        mapper.Map(updatedLocation, savedLocation);
        savedLocation.LocationId = id;

        dbContext.Locations.Update(savedLocation);

        await dbContext.SaveChangesAsync();

        return new ApiResponseDto<Locations?>
        {
            RequestFailed = false,
            ResponseCode = System.Net.HttpStatusCode.OK,
            Message = $"Location with ID: {id} updated successfully.",
            Data = savedLocation,
        };
    }

    public async Task<ApiResponseDto<string?>> DeleteLocation(int id)
    {
        Locations? savedLocation = await dbContext.Locations.FindAsync(id);

        if (savedLocation == null)
        {
            return new ApiResponseDto<string?>
            {
                RequestFailed = true,
                ResponseCode = System.Net.HttpStatusCode.NotFound,
                Message = $"Location with ID: {id} not found.",
            };
        }

        dbContext.Locations.Remove(savedLocation);
        await dbContext.SaveChangesAsync();

        return new ApiResponseDto<string?>
        {
            RequestFailed = false,
            ResponseCode = System.Net.HttpStatusCode.NoContent,
            Message = $"Location with ID: {id} deleted successfully.",
        };
    }

    public async Task FrontEndViewLocations(HttpClient httpClient)
    {
        try
        {
            var response = await httpClient.GetFromJsonAsync<ApiResponseDto<List<LocationsDto>>>(
                "api/locations"
            );
            if (response != null && response.Data != null && !response.RequestFailed)
            {
                var table = new Table()
                    .AddColumn("Name")
                    .AddColumn("Address")
                    .AddColumn("Town or City")
                    .AddColumn("State or County")
                    .AddColumn("Zip or Postcode")
                    .AddColumn("Country");
                foreach (var location in response.Data)
                {
                    table.AddRow(
                        location.Name,
                        location.Address,
                        location.TownOrCity,
                        location.StateOrCounty,
                        location.ZipOrPostCode,
                        location.Country
                    );
                }
                AnsiConsole.Write(table);
            }
            else
            {
                AnsiConsole.MarkupLine(
                    $"[red]Failed to retrieve locations: {response?.Message ?? "Unknown error"}[/]"
                );
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[red]Get all locations failed, see Exception: {ex.Message}[/]");
        }
    }

    public async Task FrontEndAddLocation(HttpClient httpClient)
    {
        var name = AnsiConsole.Ask<string>("Enter [green]Location Name[/]:");
        var address = AnsiConsole.Ask<string>("Enter [green]Address[/]:");
        var city = AnsiConsole.Ask<string>("Enter [green]Town or City[/]:");
        var state = AnsiConsole.Ask<string>("Enter [green]State or County[/]:");
        var zip = AnsiConsole.Ask<string>("Enter [green]Zip Code or Post Code[/]:");
        var country = AnsiConsole.Ask<string>("Enter [green]Country[/]:");

        try
        {
            var response = await httpClient.PostAsJsonAsync(
                "api/locations",
                new
                {
                    Name = name,
                    Address = address,
                    TownOrCity = city,
                    StateorCounty = state,
                    ZipOrPostCode = zip,
                    Country = country,
                }
            );
            if (response.IsSuccessStatusCode)
            {
                AnsiConsole.MarkupLine("[green]Location added successfully![/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Failed to add location.[/]");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Add location failed, see {ex}");
            throw;
        }
    }

    public async Task FrontEndUpdateLocation(HttpClient httpClient, int id)
    {
        var name = AnsiConsole.Confirm("Do you want to update the [green]Location Name[/]?", true)
            ? AnsiConsole.Ask<string>("Enter [green]Location Name[/]:")
            : null;

        var address = AnsiConsole.Confirm("Do you want to update the [green]Address[/]?", true)
            ? AnsiConsole.Ask<string>("Enter [green]Address[/]:")
            : null;

        var city = AnsiConsole.Confirm("Do you want to update the [green]Town or City[/]?", true)
            ? AnsiConsole.Ask<string>("Enter [green]Town or City[/]:")
            : null;

        var state = AnsiConsole.Confirm(
            "Do you want to update the [green]State or County[/]?",
            true
        )
            ? AnsiConsole.Ask<string>("Enter [green]State or County[/]:")
            : null;

        var zip = AnsiConsole.Confirm(
            "Do you want to update the [green]Zip Code or Post Code[/]?",
            true
        )
            ? AnsiConsole.Ask<string>("Enter [green]Zip Code or Post Code[/]:")
            : null;

        var country = AnsiConsole.Confirm("Do you want to update the [green]Country[/]?", true)
            ? AnsiConsole.Ask<string>("Enter [green]Country[/]:")
            : null;

        try
        {
            var response = await httpClient.PutAsJsonAsync(
                $"api/locations/{id}",
                new
                {
                    Name = name,
                    Address = address,
                    TownOrCity = city,
                    StateorCounty = state,
                    ZipOrPostCode = zip,
                    Country = country,
                }
            );
            if (response.IsSuccessStatusCode)
            {
                AnsiConsole.MarkupLine("[green]Location updated successfully![/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Failed to update location.[/]");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Update location failed, see {ex}");
            throw;
        }
    }

    public async Task FrontEndDeleteLocation(HttpClient httpClient, int id)
    {
        try
        {
            var response = await httpClient.DeleteAsync($"api/locations/{id}");
            if (response.IsSuccessStatusCode)
            {
                AnsiConsole.MarkupLine("[green]Location deleted successfully![/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Failed to delete location.[/]");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Delete location failed, see {ex}");
            throw;
        }
    }

    public async Task<ActionResult<ApiResponseDto<Locations?>>> GetLocationOptionInput(
		Data.ShiftsLoggerDbContext dbContext,
        ILocationService locationService
    )
    {
        // Get all locations
        var locationsResponse = await locationService.GetAllLocations(new LocationFilterOptions());
        var locations = locationsResponse.Data ?? new List<Locations>();

        if (locations.Count == 0)
        {
            return new ActionResult<ApiResponseDto<Locations?>>(
                new ApiResponseDto<Locations?>
                {
                    Data = null,
                    Message = "No locations available.",
                    ResponseCode = System.Net.HttpStatusCode.NotFound,
                    RequestFailed = true,
                }
            );
        }

        // Prompt user to select a location
        var selectedLocation = AnsiConsole.Prompt(
            new SelectionPrompt<Locations>()
                .Title("[yellow]Select a location:[/]")
                .UseConverter(l => $"{l.Name} ({l.Address}, {l.TownOrCity})")
                .AddChoices(locations)
        );

        if (selectedLocation == null)
        {
            return new ActionResult<ApiResponseDto<Locations?>>(
                new ApiResponseDto<Locations?>
                {
                    Data = null,
                    Message = "No location selected.",
                    ResponseCode = System.Net.HttpStatusCode.BadRequest,
                    RequestFailed = true,
                }
            );
        }
        return new ActionResult<ApiResponseDto<Locations?>>(
            new ApiResponseDto<Locations?>
            {
                Data = selectedLocation,
                Message = "Location selected successfully.",
                ResponseCode = System.Net.HttpStatusCode.OK,
                RequestFailed = false,
            }
        );
    }
}
