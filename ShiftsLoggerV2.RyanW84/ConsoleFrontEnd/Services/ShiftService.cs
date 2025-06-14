using System.Net.Http.Json;
using ConsoleFrontEnd.Models;
using ConsoleFrontEnd.Models.Dtos;
using ConsoleFrontEnd.Models.FilterOptions;
using Spectre.Console;

namespace ConsoleFrontEnd.Services;

public class ShiftService : IShiftService
{
    private readonly HttpClient httpClient = new()
    {
        BaseAddress = new Uri("https://localhost:7009/"),
    };

    public async Task<ApiResponseDto<List<Shifts>>> GetAllShifts(
        ShiftFilterOptions shiftFilterOptions
    )
    {
        try
        {
            // Debug log for incoming search parameter
            AnsiConsole.MarkupLine($"[yellow]Filter options received:[/]\n\n" +
                $"  [blue]ShiftId:[/] {shiftFilterOptions.ShiftId}\t" +
                $"  [blue]WorkerId:[/] {shiftFilterOptions.WorkerId}\t" +
                $"  [blue]LocationId:[/] {shiftFilterOptions.LocationId}\t" +
                $"  [blue]LocationName:[/] '{shiftFilterOptions.LocationName ?? "null"}'\n" +
                $"  [blue]StartTime:[/] {shiftFilterOptions.StartTime?.ToString() ?? "null"}\t" +
                $"  [blue]EndTime:[/] {shiftFilterOptions.EndTime?.ToString() ?? "null"}'\t" +
                $"  [blue]SortBy:[/] '{shiftFilterOptions.SortBy ?? "null"}'\t" +
                $"  [blue]SortOrder:[/] '{shiftFilterOptions.SortOrder ?? "null"}'\t" +
                $"  [blue]Search:[/] '{shiftFilterOptions.Search ?? "null"}'\n");

            var queryParams = new List<string>();

            // Add all filter parameters
            if (shiftFilterOptions.ShiftId != null)
                queryParams.Add($"ShiftId={shiftFilterOptions.ShiftId}");

            if (shiftFilterOptions.WorkerId != null)
                queryParams.Add($"WorkerId={shiftFilterOptions.WorkerId}");

            if (shiftFilterOptions.LocationId != null)
                queryParams.Add($"LocationId={shiftFilterOptions.LocationId}");

            // Date/time parameters
            if (shiftFilterOptions.StartTime != null)
                queryParams.Add($"StartTime={shiftFilterOptions.StartTime:O}");

            if (shiftFilterOptions.EndTime != null)
                queryParams.Add($"EndTime={shiftFilterOptions.EndTime:O}");

            if (shiftFilterOptions.StartTime != null)
                queryParams.Add($"StartDate={shiftFilterOptions.StartTime.Value.Date:yyyy-MM-dd}");

            if (shiftFilterOptions.EndTime != null)
                queryParams.Add($"EndDate={shiftFilterOptions.EndTime.Value.Date:yyyy-MM-dd}");

            // Search parameter - improved with trimming and proper null checking
            if (!string.IsNullOrWhiteSpace(shiftFilterOptions.Search?.Trim()))
            {
                queryParams.Add($"Search={Uri.EscapeDataString(shiftFilterOptions.Search.Trim())}");
                AnsiConsole.MarkupLine($"[green]Adding search parameter: '{shiftFilterOptions.Search.Trim()}'[/]");
            }

            // Other parameters
            if (!string.IsNullOrWhiteSpace(shiftFilterOptions.LocationName))
                queryParams.Add($"LocationName={Uri.EscapeDataString(shiftFilterOptions.LocationName)}");

            if (!string.IsNullOrWhiteSpace(shiftFilterOptions.SortBy))
                queryParams.Add($"SortBy={Uri.EscapeDataString(shiftFilterOptions.SortBy)}");

            if (!string.IsNullOrWhiteSpace(shiftFilterOptions.SortOrder))
                queryParams.Add($"SortOrder={Uri.EscapeDataString(shiftFilterOptions.SortOrder)}");

            // Build and log the final URL
            var queryString = "api/shifts";
            if (queryParams.Count > 0)
                queryString += "?" + string.Join("&", queryParams);

			AnsiConsole.MarkupLine($"[blue]Final request URL: {httpClient.BaseAddress}{queryString}[/]\n");

			// Make the request
			var response = await httpClient.GetAsync(queryString);

            if (response.StatusCode is System.Net.HttpStatusCode.OK)
            {
                var shifts =
                    await response.Content.ReadFromJsonAsync<ApiResponseDto<List<Shifts>>>()
                    ?? new ApiResponseDto<List<Shifts>>
                    {
                        ResponseCode = response.StatusCode,
                        Message = "Data obtained",
                        Data = [],
                    };

                return shifts;
            }
            else
            {
                var shifts =
                    await response.Content.ReadFromJsonAsync<ApiResponseDto<List<Shifts>>>()
                    ?? new ApiResponseDto<List<Shifts>>()
                    {
                        RequestFailed = true,
                        Message = $"{response.ReasonPhrase}",
                        Data = [],
                    };
                return shifts;
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
                        Data = [],
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
