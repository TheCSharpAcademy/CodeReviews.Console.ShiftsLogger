using System.Net.Http.Headers;
using System.Net.Http.Json;
using ShiftsLoggerClient.Models;

namespace ShiftsLoggerClient.Services;

class ShiftService
{
    private readonly HttpClient _httpClient;

    public ShiftService()
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7029/api/")
        };

        _httpClient.DefaultRequestHeaders.Accept.Clear();

        _httpClient.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

        _httpClient.DefaultRequestHeaders.Add("User-Agent", "ShiftsLoggerClient");
    }

    internal async Task<ApiResponse<List<ShiftDTO>>> GetAllShifts()
    {
        try
        {
            var requestUri = "shifts";

            using HttpResponseMessage response = await _httpClient.GetAsync(requestUri);

            if (!response.IsSuccessStatusCode)
            {
                string errorMessage = await response.Content.ReadAsStringAsync();
                return new ApiResponse<List<ShiftDTO>>
                {
                    Success = false,
                    Message = errorMessage
                };
            }
            var shifts = await response.Content.ReadFromJsonAsync<List<ShiftDTO>>();

            return new ApiResponse<List<ShiftDTO>>
            {
                Success = true,
                Message = "Success",
                Data = shifts
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error message: {ex.Message}");
            return new ApiResponse<List<ShiftDTO>>
            {
                Success = false,
                Message = $"Error message: {ex.Message}"
            };
        }
    }

    internal async Task<ApiResponse<ShiftDTO>> GetShiftById(long id)
    {
        try
        {
            var requestUri = $"shifts/{id}";

            using HttpResponseMessage response = await _httpClient.GetAsync(requestUri);

            if (!response.IsSuccessStatusCode)
            {
                string errorMessage = await response.Content.ReadAsStringAsync();
                return new ApiResponse<ShiftDTO>
                {
                    Success = false,
                    Message = errorMessage
                };
            }
            var shift = await response.Content.ReadFromJsonAsync<ShiftDTO>();

            return new ApiResponse<ShiftDTO>
            {
                Success = true,
                Message = "Success",
                Data = shift
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error message: {ex.Message}");
            return new ApiResponse<ShiftDTO>
            {
                Success = false,
                Message = $"Error message: {ex.Message}"
            };
        }
    }

    internal async Task<ApiResponse<ShiftDTO>> PostShift(ShiftDTO shiftDTO)
    {
        try
        {
            var requestUri = "shifts";

            var postResponse = await _httpClient.PostAsJsonAsync(requestUri, shiftDTO);

            if (!postResponse.IsSuccessStatusCode)
            {
                string errorMessage = await postResponse.Content.ReadAsStringAsync();
                return new ApiResponse<ShiftDTO>
                {
                    Success = false,
                    Message = errorMessage
                };
            }

            var createdShift = await postResponse.Content.ReadFromJsonAsync<ShiftDTO>();

            return new ApiResponse<ShiftDTO>
            {
                Success = true,
                Message = "Shift created successfully",
                Data = createdShift
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error message:{ex.Message}");
            return new ApiResponse<ShiftDTO>
            {
                Success = false,
                Message = $"Error message:{ex.Message}"
            };
        }
    }

    internal async Task<ApiResponse<ShiftDTO>> UpdateShift(ShiftDTO shiftDTO, long id)
    {
        try
        {
            var requestUri = $"shifts/{id}";

            var postResponse = await _httpClient.PutAsJsonAsync(requestUri, shiftDTO);

            if (!postResponse.IsSuccessStatusCode)
            {
                string errorMessage = await postResponse.Content.ReadAsStringAsync();
                return new ApiResponse<ShiftDTO>
                {
                    Success = false,
                    Message = errorMessage
                };
            }

            var updatedShift = await postResponse.Content.ReadFromJsonAsync<ShiftDTO>();

            return new ApiResponse<ShiftDTO>
            {
                Success = true,
                Message = "Shift has been successfully updated",
                Data = updatedShift
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error message:{ex.Message}");
            return new ApiResponse<ShiftDTO>
            {
                Success = false,
                Message = $"Error message:{ex.Message}"
            };
        }
    }

    internal async Task<ApiResponse<bool>> DeleteEmployee(long id)
    {
        try
        {
            var requestUri = $"shifts/{id}";
            using HttpResponseMessage response = await _httpClient.DeleteAsync(requestUri);

            if (response.IsSuccessStatusCode)
            {
                return new ApiResponse<bool>
                {
                    Success = true,
                    Message = "Shift has been deleted successfully",
                    StatusCode = response.StatusCode
                };
            }

            return new ApiResponse<bool>
            {
                Success = false,
                Message = await response.Content.ReadAsStringAsync(),
                StatusCode = response.StatusCode
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error message: {ex.Message}");
            return new ApiResponse<bool>
            {
                Success = false,
                Message = $"Error message: {ex.Message}"
            };
        }
    }
}