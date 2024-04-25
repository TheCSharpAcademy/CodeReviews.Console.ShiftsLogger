using Spectre.Console;
using STUDY.ASP.ShiftLoggerTryThree.Models;
using System.Net.Http.Json;

namespace STUDY.ASP.ShiftLoggerTryThreeUserInterface;
internal class ProgramEngine
{
    public static void ViewAllShiftLogs(HttpClient client, string ApiBaseUrl)
    {
        var shiftLogs = client.GetFromJsonAsync<ShiftLogger[]>(ApiBaseUrl).Result;
        if (shiftLogs != null)
        {
            var table = new Table();

            table.AddColumn(new TableColumn("Id").Centered());
            table.AddColumn(new TableColumn("First Name").Centered());
            table.AddColumn(new TableColumn("Last Name").Centered());
            table.AddColumn(new TableColumn("Clock In").Centered());
            table.AddColumn(new TableColumn("Clock Out").Centered());
            table.AddColumn(new TableColumn("Duration").Centered());

            foreach (var shiftLog in shiftLogs)
            {
                TimeSpan duration = shiftLog.ClockOut - shiftLog.ClockIn;                
                duration = TimeSpan.FromMinutes(Math.Round(duration.TotalMinutes));
                string durationString = $"{(int)duration.TotalHours}h {duration.Minutes}m";

                table.AddRow(
                    $"{shiftLog.Id}",
                    $"{shiftLog.EmployeeFirstName}",
                    $"{shiftLog.EmployeeLastName}",
                    $"{shiftLog.ClockIn}",
                    $"{shiftLog.ClockOut}",
                    $"[red]{durationString}[/]"
                );
            }

            AnsiConsole.Write(table);
        }
        else
        {
            AnsiConsole.WriteLine("No shift logs found.");
        }

        Helper.ReturnToMainMenu(client, ApiBaseUrl);
    }   
    public static void ViewSpecificShiftLog(HttpClient client, string ApiBaseUrl)
    {
        try
        {
            var id = AnsiConsole.Ask<string>("Enter Shift Log Id:");
            var specificShiftApiUrl = $"{ApiBaseUrl}/{id}";
            var shiftLog = client.GetFromJsonAsync<ShiftLogger>(specificShiftApiUrl).Result;

            if (shiftLog != null)
            {
                var table = new Table();

                table.AddColumn(new TableColumn("Id").Centered());
                table.AddColumn(new TableColumn("First Name").Centered());
                table.AddColumn(new TableColumn("Last Name").Centered());
                table.AddColumn(new TableColumn("Clock In").Centered());
                table.AddColumn(new TableColumn("Clock Out").Centered());
                table.AddColumn(new TableColumn("Duration").Centered());
                
                TimeSpan duration = shiftLog.ClockOut - shiftLog.ClockIn;
                duration = TimeSpan.FromMinutes(Math.Round(duration.TotalMinutes));
                string durationString = $"{(int)duration.TotalHours}h {duration.Minutes}m";

                table.AddRow(
                    $"{shiftLog.Id}",
                    $"{shiftLog.EmployeeFirstName}",
                    $"{shiftLog.EmployeeLastName}",
                    $"{shiftLog.ClockIn}",
                    $"{shiftLog.ClockOut}",
                    $"[red]{durationString}[/]"
                );
                
                AnsiConsole.Write(table);
            }
            else
            {
                AnsiConsole.WriteLine("No shift logs found.");
            }
        }
        catch (AggregateException ex)
        {
            AnsiConsole.WriteLine("An error occurred while retrieving the shift log. Please try again.");
            Console.WriteLine($"Error message: {ex.Message}");
        }

        Helper.ReturnToMainMenu(client, ApiBaseUrl);
    }
    public static void AddShiftLog(HttpClient client, string ApiBaseUrl)
    {
        try
        {
            var employeeFirstName = AnsiConsole.Ask<string>("Enter Employee FirstName:");
            var employeeLastName = AnsiConsole.Ask<string>("Enter Employee LastName:");
            var clockIn = Validation.ValidateClockInDateTime("Enter Clock In time (yyyy-MM-dd HH:mm):");
            var clockOut = Validation.ValidateClockOutDateTime("Enter Clock Out time (yyyy-MM-dd HH:mm):", clockIn);

            var shiftLog = new ShiftLogger
            {
                EmployeeFirstName = employeeFirstName,
                EmployeeLastName = employeeLastName,
                ClockIn = clockIn,
                ClockOut = clockOut
            };

            var response = client.PostAsJsonAsync(ApiBaseUrl, shiftLog).Result;
            if (response.IsSuccessStatusCode)
            {
                AnsiConsole.WriteLine("Shift log added successfully.");
            }
            else
            {
                AnsiConsole.WriteLine("Failed to add shift log. Please try again.");
            }
        }
        catch (AggregateException ex)
        {
            AnsiConsole.WriteLine("An error occurred while retrieving the shift log. Please try again.");
            Console.WriteLine($"Error message: {ex.Message}");
        }
        Helper.ReturnToMainMenu(client, ApiBaseUrl);
    }
    public static void UpdateShiftLog(HttpClient client, string ApiBaseUrl)
    {
        try
        {
            var id = AnsiConsole.Ask<string>("Enter Shift Log Id:");
            var specificShiftApiUrl = $"{ApiBaseUrl}/{id}";
            var shiftLogToUpdate = client.GetFromJsonAsync<ShiftLogger>(specificShiftApiUrl).Result;

            if (shiftLogToUpdate == null)
            {
                AnsiConsole.WriteLine($"Shift log with Id {id} not found.");
                Helper.ReturnToMainMenu(client, ApiBaseUrl);
                return;
            }

            var employeeFirstNameUpdated = AnsiConsole.Ask<string>("Enter Updated Employee FirstName:");
            var employeeLastNameUpdated = AnsiConsole.Ask<string>("Enter Updated Employee LastName:");
            var clockInUpdated = Validation.ValidateClockInDateTime("Enter Updated Clock In time (yyyy-MM-dd HH:mm):");
            var clockOutUpdated = Validation.ValidateClockOutDateTime("Enter Updated Clock Out time (yyyy-MM-dd HH:mm):", clockInUpdated);

            var shiftLogUpdated = new ShiftLogger
            {
                EmployeeFirstName = employeeFirstNameUpdated,
                EmployeeLastName = employeeLastNameUpdated,
                ClockIn = clockInUpdated,
                ClockOut = clockOutUpdated
            };

            var response = client.PutAsJsonAsync(specificShiftApiUrl, shiftLogUpdated).Result;
            if (response.IsSuccessStatusCode)
            {
                AnsiConsole.WriteLine($"Shift log with Id {id} updated successfully.");
            }
            else
            {
                AnsiConsole.WriteLine($"Failed to update shift log with Id {id}. Please try again.");
            }
        }

        catch (AggregateException ex)
        {
            AnsiConsole.WriteLine("An error occurred while retrieving the shift log. Please try again.");
            Console.WriteLine($"Error message: {ex.Message}");
        }

        Helper.ReturnToMainMenu(client, ApiBaseUrl);
    }
    public static void DeleteShiftLog(HttpClient client, string ApiBaseUrl)
    {
        try
        {
            var id = AnsiConsole.Ask<string>("Enter Shift Log Id:");
            var specificShiftApiUrl = $"{ApiBaseUrl}/{id}";

            var response = client.DeleteAsync(specificShiftApiUrl).Result;
            if (response.IsSuccessStatusCode)
            {
                AnsiConsole.WriteLine($"Shift log with Id {id} deleted successfully.");
            }
            else
            {
                AnsiConsole.WriteLine($"Failed to delete shift log with Id {id}. Please try again.");
            }
        }
        catch (AggregateException ex)
        {
            AnsiConsole.WriteLine("An error occurred while retrieving the shift log. Please try again.");
            Console.WriteLine($"Error message: {ex.Message}");
        }

        Helper.ReturnToMainMenu(client, ApiBaseUrl);
    }
}
