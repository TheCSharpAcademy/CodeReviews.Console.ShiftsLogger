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

        /// <summary>
        /// Retrieves and displays a list of shifts.
        /// </summary>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task ListShifts()
        {
            try
            {
                var response = await _client.GetAsync("Shift");
                if (response.IsSuccessStatusCode)
                {
                    var shifts = await response.Content.ReadFromJsonAsync<ShiftModel[]>();
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
                else
                {
                    AnsiConsole.MarkupLine($"[red]Error: Failed to retrieve shifts. Status code: {response.StatusCode}[/]");
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


        /// <summary>
        /// Starts a new shift by asking the user for confirmation and capturing the start time and employee name.
        /// It then sends a POST request to the server to create the shift and displays the ID of the created shift.
        /// </summary>
        /// <returns>A Task representing the asynchronous operation.</returns>
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

        /// <summary>
        /// Ends the specified shift by setting the end time and updating it in the database.
        /// </summary>
        /// <returns>A Task representing the asynchronous operation.</returns>
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

        /// <summary>
        /// Updates a shift with new start and end times and employee name.
        /// </summary>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task UpdateShift()
        {
            try
            {
                var id = AnsiConsole.Ask<int>("Enter Shift Id to update:");

                // Check if the shift exists before updating
                var response = await _client.GetAsync($"Shift/{id}");
                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        AnsiConsole.MarkupLine("[red]Shift not found.[/]");
                    }
                    else
                    {
                        AnsiConsole.MarkupLine($"[red]Error fetching shift: {response.ReasonPhrase}[/]");
                    }
                    return;
                }

                var existingShift = await response.Content.ReadFromJsonAsync<ShiftModel>();
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

        /// <summary>
        /// Deletes a shift by asking the user to enter the Shift ID to delete.
        /// It sends a DELETE request to the server to delete the shift with the specified ID.
        /// If the request is successful, it displays a message indicating that the shift has been deleted.
        /// If an error occurs during the request, it displays an error message.
        /// </summary>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task DeleteShift()
        {
            try
            {
                var id = AnsiConsole.Ask<int>("Enter Shift Id to delete:");

                // Check if the shift exists before deleting
                var response = await _client.GetAsync($"Shift/{id}");
                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        AnsiConsole.MarkupLine("[red]Shift not found.[/]");
                    }
                    else
                    {
                        AnsiConsole.MarkupLine($"[red]Error fetching shift: {response.ReasonPhrase}[/]");
                    }
                    return;
                }

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
