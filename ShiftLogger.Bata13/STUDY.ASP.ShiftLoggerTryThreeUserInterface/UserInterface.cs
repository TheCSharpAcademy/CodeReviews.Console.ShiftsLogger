using System.Net.Http.Json;
using Spectre.Console;
using STUDY.ASP.ShiftLoggerTryThree.Models;

namespace STUDY.ASP.ShiftLoggerTryThreeUserInterface
{
    class UserInterface
    {
        static HttpClient client = new HttpClient();
        const string ApiBaseUrl = "https://localhost:7188/api/shiftlogger";
        static void Main(string[] args)
        {
            // Your user interface logic goes here
            Console.WriteLine("Welcome to Shift Logger User Interface");

            while (true)
            {
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("What would you like to do?")
                        .AddChoices("View All Shift Logs", "Add Shift Log", "Quit"));

                switch (choice)
                {
                    case "View All Shift Logs":
                        ViewAllShiftLogs();
                        break;
                    case "Add Shift Log":
                        AddShiftLog();
                        break;
                    case "Quit":
                        Environment.Exit(0);
                        break;
                    default:
                        AnsiConsole.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
        static void ViewAllShiftLogs()
        {
            var shiftLogs = client.GetFromJsonAsync<ShiftLogger[]>(ApiBaseUrl).Result;
            if (shiftLogs != null)
            {
                foreach (var shiftLog in shiftLogs)
                {
                    TimeSpan duration = shiftLog.ClockOut - shiftLog.ClockIn;
                    AnsiConsole.WriteLine($"Id: {shiftLog.Id}, EmployeeId: {shiftLog.EmployeeId}, ClockIn: {shiftLog.ClockIn}, ClockOut: {shiftLog.ClockOut}, Duration: {duration}");
                }
            }
            else
            {
                AnsiConsole.WriteLine("No shift logs found.");
            }
        }
        static void AddShiftLog()
        {
            var employeeId = AnsiConsole.Ask<int>("Enter Employee Id:");
            var clockIn = AnsiConsole.Ask<DateTime>("Enter Clock In time (yyyy-MM-dd HH:mm:ss):");
            var clockOut = AnsiConsole.Ask<DateTime>("Enter Clock Out time (yyyy-MM-dd HH:mm:ss):");

            var shiftLog = new ShiftLogger
            {
                EmployeeId = employeeId,
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
    }
}
