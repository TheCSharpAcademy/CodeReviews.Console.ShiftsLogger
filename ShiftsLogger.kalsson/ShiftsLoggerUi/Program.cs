using System.Net.Http.Json;
using ShiftsLoggerApi.Models;
using Spectre.Console;

namespace ShiftsLoggerUi
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient { BaseAddress = new Uri("https://localhost:44310/api/") };

        static async Task Main(string[] args)
        {
            while (true)
            {
                var option = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("What do you want to do?")
                        .AddChoices("List Shifts", "Start Shift", "End Shift", "Update Shift", "Delete Shift", "Exit"));

                switch (option)
                {
                    case "List Shifts":
                        await ListShifts();
                        break;
                    case "Start Shift":
                        await StartShift();
                        break;
                    case "End Shift":
                        await EndShift();
                        break;
                    case "Update Shift":
                        await UpdateShift();
                        break;
                    case "Delete Shift":
                        await DeleteShift();
                        break;
                    case "Exit":
                        return;
                    default:
                        AnsiConsole.Markup("[red]Invalid option[/]");
                        break;
                }
            }
        }

        private static async Task ListShifts()
        {
            try
            {
                var shifts = await client.GetFromJsonAsync<ShiftModel[]>("Shift");
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

        private static async Task StartShift()
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

                    var response = await client.PostAsJsonAsync("Shift", shift);
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

        private static async Task EndShift()
        {
            try
            {
                var shiftId = AnsiConsole.Ask<int>("Enter Shift Id to end:");

                var shift = await client.GetFromJsonAsync<ShiftModel>($"Shift/{shiftId}");
                if (shift != null)
                {
                    shift.EndOfShift = DateTime.Now;
                    await client.PutAsJsonAsync($"Shift/{shiftId}", shift);
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

        private static async Task UpdateShift()
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

                await client.PutAsJsonAsync($"Shift/{id}", shift);
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

        private static async Task DeleteShift()
        {
            try
            {
                var id = AnsiConsole.Ask<int>("Enter Shift Id to delete:");

                await client.DeleteAsync($"Shift/{id}");
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
