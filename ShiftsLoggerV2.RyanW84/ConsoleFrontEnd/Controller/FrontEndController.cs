using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ShiftsLoggerV2.RyanW84.Dtos;
using Spectre.Console;

// DTOs should be imported from your shared project or defined here if not available
// using ShiftsLoggerV2.RyanW84.Dtos;

namespace ConsoleFrontEnd.Controller;

public  class FrontEndController
{
    private  readonly HttpClient _httpClient;

    public  FrontEndController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // WORKERS

    public async Task GetAllWorkersAsync()
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<ApiResponseDto<List<WorkerDto>>>(
                "api/workers"
            );
            if (response != null && response.Data != null && !response.RequestFailed)
            {
                var table = new Table().AddColumn("Name").AddColumn("Phone").AddColumn("Email");
                foreach (var worker in response.Data)
                {
                    table.AddRow(worker.Name, worker.PhoneNumber, worker.Email);
                }
                AnsiConsole.Write(table);
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Failed to retrieve workers.[/]");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"View workers failed, see {ex}");
        }
    }

    public async Task GetWorkerByIdAsync(int workerId)
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<ApiResponseDto<WorkerDto>>(
                $"api/workers/{workerId}"
            );
            if (response != null && response.Data != null && !response.RequestFailed)
            {
                var worker = response.Data;
                var table = new Table().AddColumn("Name").AddColumn("Phone").AddColumn("Email");
                table.AddRow(worker.Name, worker.PhoneNumber, worker.Email);
                AnsiConsole.Write(table);
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Failed to retrieve worker.[/]");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"View worker by ID failed, see {ex}");
        }
    }

    public async Task AddWorkerAsync(string name, string phone, string email)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync(
                "api/workers",
                new
                {
                    Name = name,
                    PhoneNumber = phone,
                    Email = email,
                }
            );
            if (response.IsSuccessStatusCode)
            {
                AnsiConsole.MarkupLine("[green]Worker added successfully![/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Failed to add worker.[/]");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Adding worker failed due to: {ex}");
        }
    }

    // SHIFTS

    public async Task GetAllShiftsAsync()
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<ApiResponseDto<List<ShiftsDto>>>(
                "api/shifts"
            );
            if (response != null && response.Data != null && !response.RequestFailed)
            {
                var table = new Table()
                    .AddColumn("Worker ID")
                    .AddColumn("Location ID")
                    .AddColumn("Start Time")
                    .AddColumn("End Time");

                foreach (var shift in response.Data)
                {
                    table.AddRow(
                        shift.WorkerId.ToString(),
                        shift.LocationId.ToString(),
                        shift.StartTime.ToString("yyyy-MM-dd HH:mm"),
                        shift.EndTime.ToString("yyyy-MM-dd HH:mm")
                    );
                }
                AnsiConsole.Write(table);
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Failed to retrieve shifts.[/]");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Get All Shifts failed, see Exception: {ex.Message}");
        }
    }

    public async Task AddShiftAsync(
        int workerId,
        int locationId,
        DateTime startTime,
        DateTime endTime
    )
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync(
                "api/shifts",
                new
                {
                    WorkerId = workerId,
                    LocationId = locationId,
                    StartTime = startTime,
                    EndTime = endTime,
                }
            );
            if (response.IsSuccessStatusCode)
            {
                AnsiConsole.MarkupLine("[green]Shift added successfully![/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Failed to add shift.[/]");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Add shift failed, see {ex}");
        }
    }

    // LOCATIONS

    public async Task GetAllLocationsAsync()
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<ApiResponseDto<List<LocationsDto>>>(
                "api/locations"
            );
            if (response != null && response.Data != null && !response.RequestFailed)
            {
                var table = new Table().AddColumn("ID").AddColumn("Name").AddColumn("Address");
                foreach (var location in response.Data)
                {
                    table.AddRow(location.LocationId.ToString(), location.Name, location.Address);
                }
                AnsiConsole.Write(table);
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Failed to retrieve locations.[/]");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"View locations failed, see {ex}");
        }
    }

    public async Task AddLocationAsync(LocationsDto NewLocationDto
       )
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync(
                "api/locations",
                NewLocationDto
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
            Console.WriteLine($"Adding location failed due to: {ex}");
        }
    }

    public LocationsDto UserInputCreateAddress()
    {
        // Gather input from the user
        var name = AnsiConsole.Ask<string>("Enter [green]Location Name[/]:");
        var address = AnsiConsole.Ask<string>("Enter [green]Address[/]:");
        var townOrCity = AnsiConsole.Ask<string>("Enter [green]Town or City[/]:");
        var stateOrCounty = AnsiConsole.Ask<string>("Enter [green]State or County[/]:");
        var zipOrPostCode = AnsiConsole.Ask<string>("Enter [green]Zip or Post Code[/]:");
        var country = AnsiConsole.Ask<string>("Enter [green]Country[/]:");

        // Create the DTO
        var NewLocationDto = new LocationsDto
        {
            Name = name,
            Address = address,
            TownOrCity = townOrCity,
            StateOrCounty= stateOrCounty,
            ZipOrPostCode = zipOrPostCode,
            Country = country
        };

       return NewLocationDto;
	}
}
