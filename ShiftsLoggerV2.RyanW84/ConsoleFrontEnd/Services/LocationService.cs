using System.Net.Http.Json;
using ConsoleFrontEnd.Models;
using ConsoleFrontEnd.Models.Dtos;
using ConsoleFrontEnd.Models.FilterOptions;

namespace ConsoleFrontEnd.Services;

public class LocationService : ILocationService
{
    private readonly HttpClient _httpClient;

    public LocationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ApiResponseDto<List<Locations?>>> GetAllLocations(LocationFilterOptions locationFilterOptions)
    {
        try
        {
            var queryParams = new List<string>();
            if (locationFilterOptions.LocationId != null)
                queryParams.Add($"LocationId={locationFilterOptions.LocationId}");
            if (!string.IsNullOrWhiteSpace(locationFilterOptions.Name))
                queryParams.Add($"Name={Uri.EscapeDataString(locationFilterOptions.Name)}");
            if (!string.IsNullOrWhiteSpace(locationFilterOptions.TownOrCity))
                queryParams.Add($"TownOrCity={Uri.EscapeDataString(locationFilterOptions.TownOrCity)}");
            if (!string.IsNullOrWhiteSpace(locationFilterOptions.StateOrCounty))
                queryParams.Add($"StateOrCounty={Uri.EscapeDataString(locationFilterOptions.StateOrCounty)}");
            if (!string.IsNullOrWhiteSpace(locationFilterOptions.Country))
                queryParams.Add($"Country={Uri.EscapeDataString(locationFilterOptions.Country)}");
            if (!string.IsNullOrWhiteSpace(locationFilterOptions.Search))
                queryParams.Add($"Search={Uri.EscapeDataString(locationFilterOptions.Search)}");
            if (!string.IsNullOrWhiteSpace(locationFilterOptions.SortBy))
                queryParams.Add($"SortBy={Uri.EscapeDataString(locationFilterOptions.SortBy)}");
            if (!string.IsNullOrWhiteSpace(locationFilterOptions.SortOrder))
                queryParams.Add($"SortOrder={Uri.EscapeDataString(locationFilterOptions.SortOrder)}");

            var queryString = "api/locations";
            if (queryParams.Count > 0)
                queryString += "?" + string.Join("&", queryParams);

            var response = await _httpClient.GetAsync(queryString);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ApiResponseDto<List<Locations?>>>()
                    ?? new ApiResponseDto<List<Locations?>>
                    {
                        RequestFailed = true,
                        ResponseCode = response.StatusCode,
                        Message = "No data returned.",
                        Data = new List<Locations?>(),
                    };
            }
            else
            {
                return new ApiResponseDto<List<Locations?>>
                {
                    RequestFailed = true,
                    ResponseCode = response.StatusCode,
                    Message = response.ReasonPhrase ?? "Error retrieving locations.",
                    Data = new List<Locations?>(),
                };
            }
        }
        catch (Exception ex)
        {
            return new ApiResponseDto<List<Locations?>>
            {
                RequestFailed = true,
                ResponseCode = System.Net.HttpStatusCode.InternalServerError,
                Message = $"Exception: {ex.Message}",
                Data = null,
            };
        }
    }

    public async Task<ApiResponseDto<List<Locations?>>> GetLocationById(int id)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/locations/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return new ApiResponseDto<List<Locations?>>
                {
                    RequestFailed = true,
                    ResponseCode = response.StatusCode,
                    Message = response.ReasonPhrase ?? "Location not found.",
                    Data = null,
                };
            }

            return await response.Content.ReadFromJsonAsync<ApiResponseDto<List<Locations?>>>()
                ?? new ApiResponseDto<List<Locations?>>
                {
                    RequestFailed = true,
                    ResponseCode = response.StatusCode,
                    Message = "No data returned.",
                    Data = new List<Locations?>(),
                };
        }
        catch (Exception ex)
        {
            return new ApiResponseDto<List<Locations?>>
            {
                RequestFailed = true,
                ResponseCode = System.Net.HttpStatusCode.InternalServerError,
                Message = $"Exception: {ex.Message}",
                Data = null,
            };
        }
    }

    public async Task<ApiResponseDto<Locations>> CreateLocation(Locations createdLocation)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/locations", createdLocation);
            if (response.StatusCode != System.Net.HttpStatusCode.Created)
            {
                return new ApiResponseDto<Locations>
                {
                    RequestFailed = true,
                    ResponseCode = response.StatusCode,
                    Message = response.ReasonPhrase ?? "Error creating location.",
                    Data = null,
                };
            }

            var location = await response.Content.ReadFromJsonAsync<Locations>();
            return new ApiResponseDto<Locations>
            {
                RequestFailed = false,
                ResponseCode = response.StatusCode,
                Message = "Location created successfully.",
                Data = location ?? createdLocation,
            };
        }
        catch (Exception ex)
        {
            return new ApiResponseDto<Locations>
            {
                RequestFailed = true,
                ResponseCode = System.Net.HttpStatusCode.InternalServerError,
                Message = $"Exception: {ex.Message}",
                Data = null,
            };
        }
    }

    public async Task<ApiResponseDto<Locations?>> UpdateLocation(int id, Locations updatedLocation)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"api/locations/{id}", updatedLocation);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return new ApiResponseDto<Locations?>
                {
                    RequestFailed = true,
                    ResponseCode = response.StatusCode,
                    Message = response.ReasonPhrase ?? "Error updating location.",
                    Data = null,
                };
            }

            var location = await response.Content.ReadFromJsonAsync<Locations>();
            return new ApiResponseDto<Locations?>
            {
                RequestFailed = false,
                ResponseCode = response.StatusCode,
                Message = "Location updated successfully.",
                Data = location ?? updatedLocation,
            };
        }
        catch (Exception ex)
        {
            return new ApiResponseDto<Locations?>
            {
                RequestFailed = true,
                ResponseCode = System.Net.HttpStatusCode.InternalServerError,
                Message = $"Exception: {ex.Message}",
                Data = null,
            };
        }
    }

    public async Task<ApiResponseDto<string?>> DeleteLocation(int id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"api/locations/{id}");
            if (response.StatusCode != System.Net.HttpStatusCode.NoContent)
            {
                return new ApiResponseDto<string?>
                {
                    RequestFailed = true,
                    ResponseCode = response.StatusCode,
                    Message = response.ReasonPhrase ?? "Error deleting location.",
                    Data = null,
                };
            }

            return new ApiResponseDto<string?>
            {
                RequestFailed = false,
                ResponseCode = response.StatusCode,
                Message = "Location deleted successfully.",
                Data = null,
            };
        }
        catch (Exception ex)
        {
            return new ApiResponseDto<string?>
            {
                RequestFailed = true,
                ResponseCode = System.Net.HttpStatusCode.InternalServerError,
                Message = $"Exception: {ex.Message}",
                Data = null,
            };
        }
    }
}