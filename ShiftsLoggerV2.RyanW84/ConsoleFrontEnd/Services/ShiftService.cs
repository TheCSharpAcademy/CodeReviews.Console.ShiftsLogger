using System.Net.Http.Json;
using ConsoleFrontEnd.Models;
using ConsoleFrontEnd.Models.Dtos;
using ConsoleFrontEnd.Models.FilterOptions;
using Spectre.Console;

namespace ConsoleFrontEnd.Services;

public class ShiftService : IShiftService
{
    private readonly HttpClient httpClient = new HttpClient()
    {
        BaseAddress = new Uri("https://localhost:7009/"),
    };

    public async Task<ApiResponseDto<List<Shifts>>> GetAllShifts(
        ShiftFilterOptions shiftFilterOptions
    )
    {
        HttpResponseMessage response;
        try
        {
            var queryString =
                $"api/shifts?workerId={shiftFilterOptions.WorkerId}&locationId={shiftFilterOptions.LocationId}&startTime={shiftFilterOptions.StartTime}&endTime={shiftFilterOptions.EndTime}";

            response = await httpClient.GetAsync(queryString);
            if (!response.IsSuccessStatusCode)
            {
                return new ApiResponseDto<List<Shifts>>
                {
                    ResponseCode = response.StatusCode,
                    Message = response.ReasonPhrase,
                    Data = null,
                };
            }
            else if (response.StatusCode is System.Net.HttpStatusCode.NoContent)
            {
                Console.WriteLine("No shifts found.");
                return new ApiResponseDto<List<Shifts>>
                {
                    ResponseCode = response.StatusCode,
                    Message = "No shifts found.",
                    Data = new List<Shifts>(),
                    TotalCount = 0,
                };
            }
            else
            {
                var createdShift =
                    await response.Content.ReadFromJsonAsync<ApiResponseDto<List<Shifts>>>()
                    ?? new ApiResponseDto<List<Shifts>>
                    {
                        ResponseCode = response.StatusCode,
                        Message = "No data returned.",
                        Data = new List<Shifts>(),
                        TotalCount = 0,
                    };

                return createdShift;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Try catch failed for GetAllShifts: {ex}");
            throw;
        }
    }

    public async Task<ApiResponseDto<List<Shifts>>> GetShiftById(int id)
    {
        HttpResponseMessage response;
        try
        {
            response = await httpClient.GetAsync($"api/shifts/{id}");

            if (response.StatusCode is not System.Net.HttpStatusCode.OK)
            {
                AnsiConsole.Markup($"\n[Red]Error - {response.StatusCode}[/]\n");

                return new ApiResponseDto<List<Shifts>>
                {
                    ResponseCode = response.StatusCode,
                    Message = response.ReasonPhrase,
                    Data = null,
                };
            }
            else
            {
                AnsiConsole.Markup("\n[Green]Shift retrieved successfully[/]\n");
                return await response.Content.ReadFromJsonAsync<ApiResponseDto<List<Shifts>>>()
                    ?? new ApiResponseDto<List<Shifts>>
                    {
                        ResponseCode = response.StatusCode,
                        Message = "No data returned.",
                        Data = new (),
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

    public async Task<ApiResponseDto<Shifts>> CreateShift(Shifts createdShift)
    {
        HttpResponseMessage response;
        try
        {
            response = await httpClient.PostAsJsonAsync("api/shifts", createdShift);
            if (
                response.StatusCode is not System.Net.HttpStatusCode.OK
                || response.StatusCode is not System.Net.HttpStatusCode.Created
            )
            {
                Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                return new ApiResponseDto<Shifts>
                {
                    ResponseCode = response.StatusCode,
                    Message = response.ReasonPhrase,
                    Data = null,
                };
            }
            else
            {
                Console.WriteLine("Shift created successfully.");
                return new ApiResponseDto<Shifts>
                {
                    ResponseCode = response.StatusCode,
                    Data = createdShift,
                };
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Try catch failed for CreateShift: {ex}");
            throw;
        }
    }

    public async Task<ApiResponseDto<Shifts?>> UpdateShift(int id, Shifts updatedShift)
    {
        HttpResponseMessage response;
        try
        {
            response = await httpClient.PutAsJsonAsync($"api/shifts/{id}", updatedShift);
            if (response.StatusCode is not System.Net.HttpStatusCode.OK)
            {
                AnsiConsole.Markup($"\n[red]Error - {response.StatusCode}[/]\n");
                return new ApiResponseDto<Shifts>
                {
                    ResponseCode = response.StatusCode,
                    Message = response.ReasonPhrase,
                    Data = null,
                };
            }
            else
            {
                AnsiConsole.Markup("\n[Green]Shift retrieved successfully[/]\n");
                return await response.Content.ReadFromJsonAsync<ApiResponseDto<Shifts>>()
                    ?? new ApiResponseDto<Shifts>
                    {
                        ResponseCode = response.StatusCode,
                        Message = "Update Shift succeeded.",
                        Data = null,
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
        HttpResponseMessage response;
        try
        {
            response = await httpClient.DeleteAsync($"api/shifts/{id}");
            if (response.StatusCode is System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine($"Error - {response.StatusCode}");
                return new ApiResponseDto<string>
                {
                    ResponseCode = response.StatusCode,
                    Message = "Shift not found.",
                    Data = null,
                };
            }
            else if (response.StatusCode is System.Net.HttpStatusCode.NoContent)
            {
                AnsiConsole.Markup("\n[Green]Shift deleted successfully![/]\n");
                return new ApiResponseDto<string>
                {
                    ResponseCode = response.StatusCode,
                    Message = response.ReasonPhrase,
                    Data = null,
                };
            }
            else
            {
                AnsiConsole.Markup($"[red]Error - {response.StatusCode}[/]");
                return new ApiResponseDto<string>
                {
                    ResponseCode = response.StatusCode,
                    Message = response.ReasonPhrase,
                    Data = null,
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
