using System.Net.Http.Json;
using AutoMapper;
using ConsoleFrontEnd.ApiShiftService;
using ShiftsLoggerV2.RyanW84.Dtos;
using ShiftsLoggerV2.RyanW84.Models;
using ShiftsLoggerV2.RyanW84.Models.FilterOptions;
using ShiftsLoggerV2.RyanW84.Services;

namespace ConsoleFrontEnd.Services;

public class ShiftService : IShiftService
{
    private static HttpClient httpClient = new HttpClient();

    public async Task<ApiResponseDto<List<ShiftsDto>>> GetAllShifts(ShiftFilterOptions shiftOptions)
    {
        HttpResponseMessage response;
        try
        {
            response = await _httpClient.GetAsync($"api/shifts?{shiftOptions}");

            var result = await response.Content.ReadFromJsonAsync<
                ApiResponseDto<List<ShiftsDto>>
            >();
            if (result is not null && result.Data is not null)
            {
                return new ApiResponseDto<List<ShiftsDto>>
                {
                    RequestFailed = result.RequestFailed,
                    ResponseCode = result.ResponseCode,
                    Message = result.Message,
                    Data = result.Data,
                    TotalCount = result.TotalCount,
                };
            }
            else
            {
                return new ApiResponseDto<List<ShiftsDto>>
                {
                    RequestFailed = true,
                    Message = "No data returned.",
                    ResponseCode = System.Net.HttpStatusCode.NoContent,
                    Data = null,
                    TotalCount = 0,
                };
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Try catch failed for GetAllShifts: {ex}");
            throw;
        }
    }

    public async Task<ApiResponseDto<ShiftsDto?>> GetShiftById(int id)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/shifts/{id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ApiResponseDto<ShiftsDto?>>();
                if (result is not null)
                {
                    return new ApiResponseDto<ShiftsDto?>
                    {
                        RequestFailed = result.RequestFailed,
                        ResponseCode = result.ResponseCode,
                        Message = result.Message,
                        Data = result.Data,
                        TotalCount = result.TotalCount,
                    };
                }
                else
                {
                    return new ApiResponseDto<ShiftsDto?>
                    {
                        RequestFailed = true,
                        Message = "No data returned.",
                        ResponseCode = response.StatusCode,
                        Data = null,
                        TotalCount = 0,
                    };
                }
            }
            else
            {
                return new ApiResponseDto<ShiftsDto?>
                {
                    RequestFailed = true,
                    Message = "Unsuccessful HTTP request",
                    ResponseCode = response.StatusCode,
                    Data = null,
                    TotalCount = 0,
                };
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Try catch failed for GetShiftById: {ex}");
            throw;
        }
    }

    public async Task<ApiResponseDto<ShiftsDto>> CreateShift(ShiftApiRequestDto shift)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/shifts", shift);
            if (response.StatusCode is not System.Net.HttpStatusCode.Created)
            {
                ApiResponseDto<ShiftsDto>? result = await response.Content.ReadFromJsonAsync<
                    ApiResponseDto<ShiftsDto>
                >();
                return result
                    ?? new ApiResponseDto<ShiftsDto>
                    {
                        RequestFailed = true,
                        Message = "No data returned.",
                    };
            }
            else
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<
                    ApiResponseDto<ShiftsDto>
                >();
                return new ApiResponseDto<ShiftsDto>
                {
                    RequestFailed = true,
                    Message = errorResponse?.Message ?? "Unknown error.",
                };
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Try catch failed for CreateShift: {ex}");
            throw;
        }
    }

    public async Task<ApiResponseDto<ShiftsDto>> UpdateShift(
        int id,
        ShiftApiRequestDto updatedShift
    )
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"api/shifts/{id}", updatedShift);
            if (response.IsSuccessStatusCode)
            {
                ApiResponseDto<ShiftsDto?>? result = await response.Content.ReadFromJsonAsync<
                    ApiResponseDto<ShiftsDto?>
                >();
                return result
                    ?? new ApiResponseDto<ShiftsDto?>
                    {
                        RequestFailed = true,
                        Message = "No data returned.",
                    };
            }
            else
            {
                ApiResponseDto<ShiftsDto?>? errorResponse =
                    await response.Content.ReadFromJsonAsync<ApiResponseDto<ShiftsDto?>>();
                return new ApiResponseDto<ShiftsDto?>
                {
                    RequestFailed = true,
                    Message = errorResponse?.Message ?? "Unknown error.",
                    ResponseCode = response.StatusCode,
                    Data = null,
                    TotalCount = 0,
                };
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Try catch failed for UpdateShift: {ex}");
            throw;
        }
    }

    public async Task<ApiResponseDto<string?>> DeleteShift(int id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"api/shifts/{id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ApiResponseDto<string?>>();
                return result
                    ?? new ApiResponseDto<string?>
                    {
                        RequestFailed = true,
                        Message = "No data returned.",
                    };
            }
            else
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<
                    ApiResponseDto<string?>
                >();
                return new ApiResponseDto<string?>
                {
                    RequestFailed = true,
                    Message = errorResponse?.Message ?? "Unknown error.",
                };
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Try catch failed for DeleteShift: {ex}");
            throw;
        }
    }
}
