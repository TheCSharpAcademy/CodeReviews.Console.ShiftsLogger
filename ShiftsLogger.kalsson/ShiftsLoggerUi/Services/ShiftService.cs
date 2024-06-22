using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ShiftsLoggerApi.Models;
using Spectre.Console;

namespace ShiftsLoggerUi.Services
{
    public class ShiftService
    {
        private readonly HttpClient _client;

        public ShiftService(HttpClient client)
        {
            _client = client;
        }

        public async Task ListShifts()
        {
            try
            {
                var shifts = await _client.GetFromJsonAsync<ShiftModel[]>("Shift");
                if (shifts != null && shifts.Length > 0)
                {
                    foreach (var shift in shifts)
                    {
                        AnsiConsole.MarkupLine($"Id: [green]{shift.Id}[/], Start: [blue]{shift.StartOfShift}[/], End: [blue]{shift.EndOfShift}[/], Duration: [blue]{shift.ShiftDuration}[/], EmployeeName: [blue]{shift.EmployeeName}[/]");
                    }
                }
                else
                {
                    AnsiConsole.MarkupLine("[yellow]No shifts found.[/]");
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Error:[/] {ex.Message}");
            }
        }

        public async Task StartShift()
        {
            try
            {
                var confirm = AnsiConsole.Confirm("Do you want to start your shift?");
                if (confirm)
                {
                    var shift = new ShiftModel
                    {
                        StartOfShift = DateTime.Now,
                        EmployeeName = AnsiConsole.Ask<string>("Enter Employee Name:")
                    };

                    var response = await _client.PostAsJsonAsync("Shift", shift);
                    response.EnsureSuccessStatusCode();

                    var createdShift = await response.Content.ReadFromJsonAsync<ShiftModel>();
                    AnsiConsole.MarkupLine($"Shift started with Id: [green]{createdShift.Id}[/]");
                }
            }
            catch (HttpRequestException httpRequestException)
            {
                AnsiConsole.MarkupLine($"[red]HTTP Request Error:[/] {httpRequestException.Message}");
                if (httpRequestException.InnerException != null)
                {
                    AnsiConsole.MarkupLine($"[red]Inner Exception:[/] {httpRequestException.InnerException.Message}");
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Error:[/] {ex.Message}");
            }
        }

        public async Task EndShift()
        {
            try
            {
                var shiftId = AnsiConsole.Ask<int>("Enter Shift Id to end:");

                var shift = await _client.GetFromJsonAsync<ShiftModel>($"Shift/{shiftId}");
                if (shift != null)
                {
                    shift.EndOfShift = DateTime.Now;
                    await _client.PutAsJsonAsync($"Shift/{shiftId}", shift);
                    AnsiConsole.MarkupLine("Shift ended successfully.");
                }
                else
                {
                    AnsiConsole.MarkupLine("[red]Shift not found.[/]");
                }
            }
            catch (HttpRequestException httpRequestException)
            {
                AnsiConsole.MarkupLine($"[red]HTTP Request Error:[/] {httpRequestException.Message}");
                if (httpRequestException.InnerException != null)
                {
                    AnsiConsole.MarkupLine($"[red]Inner Exception:[/] {httpRequestException.InnerException.Message}");
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Error:[/] {ex.Message}");
            }
        }

        public async Task UpdateShift()
        {
            try
            {
                var id = AnsiConsole.Ask<int>("Enter Shift Id to update:");
                var startShift = AnsiConsole.Ask<DateTime>("Enter new start shift time (yyyy-MM-dd HH:mm:ss):");
                var endShift = AnsiConsole.Ask<DateTime>("Enter new end shift time (yyyy-MM-dd HH:mm:ss):");
                var employeeName = AnsiConsole.Ask<string>("Enter Employee Name:");

                var shift = new ShiftModel
                {
                    Id = id,
                    StartOfShift = startShift,
                    EndOfShift = endShift,
                    EmployeeName = employeeName
                };

                await _client.PutAsJsonAsync($"Shift/{id}", shift);
                AnsiConsole.MarkupLine("Shift updated successfully.");
            }
            catch (HttpRequestException httpRequestException)
            {
                AnsiConsole.MarkupLine($"[red]HTTP Request Error:[/] {httpRequestException.Message}");
                if (httpRequestException.InnerException != null)
                {
                    AnsiConsole.MarkupLine($"[red]Inner Exception:[/] {httpRequestException.InnerException.Message}");
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Error:[/] {ex.Message}");
            }
        }

        public async Task DeleteShift()
        {
            try
            {
                var id = AnsiConsole.Ask<int>("Enter Shift Id to delete:");

                await _client.DeleteAsync($"Shift/{id}");
                AnsiConsole.MarkupLine("Shift deleted successfully.");
            }
            catch (HttpRequestException httpRequestException)
            {
                AnsiConsole.MarkupLine($"[red]HTTP Request Error:[/] {httpRequestException.Message}");
                if (httpRequestException.InnerException != null)
                {
                    AnsiConsole.MarkupLine($"[red]Inner Exception:[/] {httpRequestException.InnerException.Message}");
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Error:[/] {ex.Message}");
            }
        }
    }
}
