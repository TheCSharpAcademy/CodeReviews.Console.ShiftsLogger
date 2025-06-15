using System.Net.Http.Json;
using ConsoleFrontEnd.Models;
using ConsoleFrontEnd.Models.Dtos;
using ConsoleFrontEnd.Models.FilterOptions;

namespace ConsoleFrontEnd.Services;

public class ShiftService : IShiftService
{
    private readonly HttpClient _httpClient;

    public ShiftService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ApiResponseDto<List<Shifts?>>> GetAllShifts(
        ShiftFilterOptions shiftFilterOptions
    )
    {
        try
        {
            var queryParams = new List<string>();

            if (shiftFilterOptions.ShiftId != null)
                queryParams.Add($"ShiftId={shiftFilterOptions.ShiftId}");
            if (shiftFilterOptions.WorkerId != null)
                queryParams.Add($"WorkerId={shiftFilterOptions.WorkerId}");
            if (shiftFilterOptions.LocationId != null)
                queryParams.Add($"LocationId={shiftFilterOptions.LocationId}");
            if (shiftFilterOptions.StartTime != null)
                queryParams.Add($"StartTime={shiftFilterOptions.StartTime:O}");
            if (shiftFilterOptions.EndTime != null)
                queryParams.Add($"EndTime={shiftFilterOptions.EndTime:O}");
            if (shiftFilterOptions.StartTime != null)
                queryParams.Add($"StartDate={shiftFilterOptions.StartTime.Value.Date:yyyy-MM-dd}");
            if (shiftFilterOptions.EndTime != null)
                queryParams.Add($"EndDate={shiftFilterOptions.EndTime.Value.Date:yyyy-MM-dd}");
            if (!string.IsNullOrWhiteSpace(shiftFilterOptions.Search))
                queryParams.Add($"Search={Uri.EscapeDataString(shiftFilterOptions.Search.Trim())}");
            if (!string.IsNullOrWhiteSpace(shiftFilterOptions.LocationName))
                queryParams.Add(
                    $"LocationName={Uri.EscapeDataString(shiftFilterOptions.LocationName)}"
                );
            if (!string.IsNullOrWhiteSpace(shiftFilterOptions.SortBy))
                queryParams.Add($"SortBy={Uri.EscapeDataString(shiftFilterOptions.SortBy)}");
            if (!string.IsNullOrWhiteSpace(shiftFilterOptions.SortOrder))
                queryParams.Add($"SortOrder={Uri.EscapeDataString(shiftFilterOptions.SortOrder)}");

            var queryString = "api/shifts";
            if (queryParams.Count > 0)
                queryString += "?" + string.Join("&", queryParams);

            var response = await _httpClient.GetAsync(queryString);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ApiResponseDto<List<Shifts?>>>()
                    ?? new ApiResponseDto<List<Shifts?>>
                    {
                        RequestFailed = true,
                        ResponseCode = response.StatusCode,
                        Message = "No data returned.",
                        Data = new List<Shifts?>(),
                    };
            }
            else
            {
                return new ApiResponseDto<List<Shifts?>>
                {
                    RequestFailed = true,
                    ResponseCode = response.StatusCode,
                    Message = response.ReasonPhrase ?? "Error retrieving shifts.",
                    Data = new List<Shifts?>(),
                };
            }
        }
        catch (Exception ex)
        {
            return new ApiResponseDto<List<Shifts?>>
            {
                RequestFailed = true,
                ResponseCode = System.Net.HttpStatusCode.InternalServerError,
                Message = $"Exception: {ex.Message}",
                Data = null,
            };
        }
    }

    public async Task<ApiResponseDto<List<Shifts?>>> GetShiftById(int id)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/shifts/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return new ApiResponseDto<List<Shifts?>>
                {
                    RequestFailed = true,
                    ResponseCode = response.StatusCode,
                    Message = response.ReasonPhrase ?? "Shift not found.",
                    Data = null,
                };
            }

            return await response.Content.ReadFromJsonAsync<ApiResponseDto<List<Shifts?>>>()
                ?? new ApiResponseDto<List<Shifts?>>
                {
                    RequestFailed = true,
                    ResponseCode = response.StatusCode,
                    Message = "No data returned.",
                    Data = new List<Shifts?>(),
                };
        }
        catch (Exception ex)
        {
            return new ApiResponseDto<List<Shifts?>>
            {
                RequestFailed = true,
                ResponseCode = System.Net.HttpStatusCode.InternalServerError,
                Message = $"Exception: {ex.Message}",
                Data = null,
            };
        }
    }

    public async Task<ApiResponseDto<Shifts>> CreateShift(Shifts createdShift)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/shifts", createdShift);
            if (response.StatusCode != System.Net.HttpStatusCode.Created)
            {
                return new ApiResponseDto<Shifts>
                {
                    RequestFailed = true,
                    ResponseCode = response.StatusCode,
                    Message = response.ReasonPhrase ?? "Error creating shift.",
                    Data = null,
                };
            }

            var shift = await response.Content.ReadFromJsonAsync<Shifts>();
            return new ApiResponseDto<Shifts>
            {
                RequestFailed = false,
                ResponseCode = response.StatusCode,
                Message = "Shift created successfully.",
                Data = shift ?? createdShift,
            };
        }
        catch (Exception ex)
        {
            return new ApiResponseDto<Shifts>
            {
                RequestFailed = true,
                ResponseCode = System.Net.HttpStatusCode.InternalServerError,
                Message = $"Exception: {ex.Message}",
                Data = null,
            };
        }
    }

    public async Task<ApiResponseDto<Shifts?>> UpdateShift(int id, Shifts updatedShift)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"api/shifts/{id}", updatedShift);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return new ApiResponseDto<Shifts?>
                {
                    RequestFailed = true,
                    ResponseCode = response.StatusCode,
                    Message = response.ReasonPhrase ?? "Error updating shift.",
                    Data = null,
                };
            }

            var shift = await response.Content.ReadFromJsonAsync<Shifts>();
            return new ApiResponseDto<Shifts?>
            {
                RequestFailed = false,
                ResponseCode = response.StatusCode,
                Message = "Shift updated successfully.",
                Data = shift ?? updatedShift,
            };
        }
        catch (Exception ex)
        {
            return new ApiResponseDto<Shifts?>
            {
                RequestFailed = true,
                ResponseCode = System.Net.HttpStatusCode.InternalServerError,
                Message = $"Exception: {ex.Message}",
                Data = null,
            };
        }
    }

    public async Task<ApiResponseDto<string?>> DeleteShift(int id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"api/shifts/{id}");
            if (response.StatusCode != System.Net.HttpStatusCode.NoContent)
            {
                return new ApiResponseDto<string?>
                {
                    RequestFailed = true,
                    ResponseCode = response.StatusCode,
                    Message = response.ReasonPhrase ?? "Error deleting shift.",
                    Data = null,
                };
            }

            return new ApiResponseDto<string?>
            {
                RequestFailed = false,
                ResponseCode = response.StatusCode,
                Message = "Shift deleted successfully.",
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
