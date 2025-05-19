using System.Diagnostics;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShiftsLoggerV2.RyanW84.Data;
using ShiftsLoggerV2.RyanW84.Dtos;
using ShiftsLoggerV2.RyanW84.Models;
using ShiftsLoggerV2.RyanW84.Models.FilterOptions;

using Spectre.Console;

namespace ShiftsLoggerV2.RyanW84.Services;

public class LocationService(ShiftsDbContext dbContext, IMapper mapper) : ILocationService
{
    public async Task<ApiResponseDto<List<Locations>>> GetAllLocations(LocationFilterOptions locationOptions)
    {
        var query = dbContext.Locations.Include(l => l.Shifts).AsQueryable();

        List<Locations>? locations;

        if (!string.IsNullOrEmpty(locationOptions.LocationId.ToString()))
        {
            query = query.Where(l => l.LocationId.ToString() == locationOptions.LocationId.ToString());
        }

        if (locationOptions.SortBy == "location_id" || !string.IsNullOrEmpty(locationOptions.SortBy))
        {
            if (!string.IsNullOrEmpty(locationOptions.SortBy))
            {
                var sortBy = locationOptions.SortBy.ToLowerInvariant();
                var sortOrder = locationOptions.SortOrder?.ToLowerInvariant() ?? "asc";

                switch (sortBy)
                {
                    case "location_id":
                        query = locationOptions.SortOrder.Equals("ASC", StringComparison.CurrentCultureIgnoreCase)
                            ? query.OrderBy(l => l.LocationId)
                            : query.OrderByDescending(l => l.LocationId);
                        break;
                    default:
                        query = locationOptions.SortOrder.Equals("ASC", StringComparison.CurrentCultureIgnoreCase)
                            ? query.OrderBy(l => l.LocationId)
                            : query.OrderByDescending(l => l.LocationId);
                        break;
                }
            }
        }

        if (!string.IsNullOrEmpty(locationOptions.Search))
        {
            string searchLower = locationOptions.Search.ToLower();
            var data = await query.ToListAsync();

            locations = data.Where(l =>
                    l.LocationId.ToString().Contains(searchLower)
                    || l.Name.ToLower().Contains(searchLower)
                    || l.Address.ToLower().Contains(searchLower)
                    || l.TownOrCity.ToLower().Contains(searchLower)
                    || l.StateOrCounty.ToLower().Contains(searchLower)
                    || l.ZipOrPostCode.ToLower().Contains(searchLower)
                    || l.Country.ToLower().Contains(searchLower)
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

    public async Task<ApiResponseDto<Locations?>> UpdateLocation(int id, LocationApiRequestDto updatedLocation)
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
}
