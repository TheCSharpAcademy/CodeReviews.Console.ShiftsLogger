using System.Net.Http.Json;
using AutoMapper;
using ShiftsLoggerV2.RyanW84.Dtos;
using ShiftsLoggerV2.RyanW84.Models;
using ShiftsLoggerV2.RyanW84.Models.FilterOptions;
using ShiftsLoggerV2.RyanW84.Services;

namespace ConsoleFrontEnd.Services;

public class ShiftService : IShiftService
{
    private readonly HttpClient _httpClient;
    private readonly IMapper _mapper;

    public ShiftService(HttpClient httpClient, IMapper mapper)
    {
        _httpClient = httpClient;
        _mapper = mapper;
        _httpClient.BaseAddress = new Uri("https://localhost:7009");
    }

    public async Task<ApiResponseDto<List<Shifts>>> GetAllShifts(ShiftFilterOptions shiftOptions)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/shifts?{shiftOptions}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<
                    ApiResponseDto<List<ShiftsDto>>
                >();
                if (result != null && result.Data != null)
                {
                    // Map List<ShiftsDto> to List<Shifts>
                    var shifts = _mapper.Map<List<Shifts>>(result.Data);
                    return new ApiResponseDto<List<Shifts>>
                    {
                        RequestFailed = result.RequestFailed,
                        ResponseCode = result.ResponseCode,
                        Message = result.Message,
                        Data = shifts,
                        TotalCount = result.TotalCount,
                    };
                }
                else
                {
                    return new ApiResponseDto<List<Shifts>>
                    {
                        RequestFailed = true,
                        Message = "No data returned.",
                    };
                }
            }
            else
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<
                    ApiResponseDto<List<Shifts>>
                >();
                return new ApiResponseDto<List<Shifts>>
                {
                    RequestFailed = true,
                    Message = errorResponse?.Message ?? "Unknown error.",
                };
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Try catch failed for GetAllShifts: {ex}");
            throw;
        }
    }

    public async Task<ApiResponseDto<Shifts?>> GetShiftById(int id)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/shifts/{id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ApiResponseDto<ShiftsDto?>>();
                if (result != null && result.Data != null)
                {
                    // Use AutoMapper to map ShiftsDto to Shifts
                    var shift = _mapper.Map<Shifts>(result.Data);
                    return new ApiResponseDto<Shifts?>
                    {
                        RequestFailed = result.RequestFailed,
                        ResponseCode = result.ResponseCode,
                        Message = result.Message,
                        Data = shift,
                        TotalCount = result.TotalCount,
                    };
                }
                else
                {
                    return new ApiResponseDto<Shifts?>
                    {
                        RequestFailed = true,
                        Message = "No data returned.",
                    };
                }
            }
            else
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<
                    ApiResponseDto<ShiftsDto?>
                >();
                return new ApiResponseDto<Shifts?>
                {
                    RequestFailed = true,
                    Message = errorResponse?.Message ?? "Unknown error.",
                };
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Try catch failed for GetShiftById: {ex}");
            throw;
        }
    }

    public async Task<ApiResponseDto<Shifts>> CreateShift(ShiftApiRequestDto shift)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("api/shifts", shift);
            if (!response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ApiResponseDto<Shifts>>();
                return result
                    ?? new ApiResponseDto<Shifts>
                    {
                        RequestFailed = true,
                        Message = "No data returned.",
                    };
            }
            else
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<
                    ApiResponseDto<Shifts>
                >();
                return new ApiResponseDto<Shifts>
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

    public async Task<ApiResponseDto<Shifts?>> UpdateShift(int id, ShiftApiRequestDto updatedShift)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"api/shifts/{id}", updatedShift);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ApiResponseDto<Shifts?>>();
                return result
                    ?? new ApiResponseDto<Shifts?>
                    {
                        RequestFailed = true,
                        Message = "No data returned.",
                    };
            }
            else
            {
                var errorResponse = await response.Content.ReadFromJsonAsync<
                    ApiResponseDto<Shifts?>
                >();
                return new ApiResponseDto<Shifts?>
                {
                    RequestFailed = true,
                    Message = errorResponse?.Message ?? "Unknown error.",
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
