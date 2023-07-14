using Kmakai.ShiftsLogger.Models;
using System.Net.Http.Json;
using Spectre.Console;
using Newtonsoft.Json;

namespace Kmakai.ShiftsLoggerUI.Services;

public class ShiftsLoggerService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl = "https://localhost:7168/";

    public ShiftsLoggerService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Shift>> GetShiftsAsync()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<Shift>>(_baseUrl + "api/shifts");
    }

    public void ListShifts()
    {
        Console.Clear();
       
        var shiftsUnfiltered = GetShiftsAsync().Result;

        var name = AnsiConsole.Ask<string>("Enter employee name to filter shifts by [green]enter x to show all[/]:");

        var table = new Table();
        table.AddColumn("Employee Name");
        table.AddColumn("Date");
        table.AddColumn("Punch-In");
        table.AddColumn("Punch-Out");
        table.AddColumn("Total Hours");

        if (name.ToLower() == "x")
        {
            foreach (var shift in shiftsUnfiltered)
            {
                table.AddRow(shift.EmployeeName, shift.StartTime.ToShortDateString(), shift.StartTime.TimeOfDay.ToString("hh':'mm"), shift.EndTime?.TimeOfDay.ToString("hh':'mm") ?? "", shift.Duration?.ToString("hh'.'mm") ?? "");
            }
        }
        else
        {
            var shifts = shiftsUnfiltered.Where(s => s.EmployeeName == name.ToLower());
            foreach (var shift in shifts)
            {
                table.AddRow(shift.EmployeeName, shift.StartTime.ToShortDateString(), shift.StartTime.TimeOfDay.ToString("hh':'mm"), shift.EndTime?.TimeOfDay.ToString("hh':'mm") ?? "", shift.Duration?.ToString("hh'.'mm") ?? "");
            }
        }


        AnsiConsole.Write(table);

        Console.WriteLine();
        Console.WriteLine("press any key to exit");
        Console.ReadKey();

    }

    public async Task<Shift> GetShiftAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<Shift>(_baseUrl + $"api/shifts/{id}");
    }

    public async Task<Shift> AddShiftAsync(Shift shift)
    {
        Console.WriteLine(JsonConvert.SerializeObject(shift));
        var response = await _httpClient.PostAsJsonAsync(_baseUrl + $"api/shifts", shift);
        return await response.Content.ReadFromJsonAsync<Shift>();
    }
    public void ClockIn()
    {
        var name = AnsiConsole.Ask<string>("Enter employee name:").ToLower();

        var shift = new Shift { EmployeeName = name, StartTime = DateTime.Now };

        var newShift = AddShiftAsync(shift).Result;

        Console.WriteLine($"\nShift for {newShift.EmployeeName} started at {newShift.StartTime.ToShortTimeString()}");

        Console.WriteLine();
        Console.WriteLine("press any key to exit");
        Console.ReadKey();
    }
    public async Task UpdateShiftAsync(Shift shift)
    {
        await _httpClient.PutAsJsonAsync(_baseUrl + $"api/shifts/{shift.Id}", shift);
    }
    public void ClockOut()
    {
        var name = AnsiConsole.Ask<string>("Enter employee name:").ToLower();
        var shift = new Shift { EmployeeName = name, EndTime = DateTime.Now };
        var shifts = GetShiftsAsync().Result;
        var shiftToUpdate = shifts.Where(s => s.EmployeeName == name && s.EndTime == null).FirstOrDefault();
        if (shiftToUpdate != null)
        {
            shiftToUpdate.EndTime = shift.EndTime;
            UpdateShiftAsync(shiftToUpdate).Wait();
            Console.WriteLine($"\nShift for {shiftToUpdate.EmployeeName} ended at {shiftToUpdate.EndTime?.ToShortTimeString()}");

        }
        else
        {
            Console.WriteLine($"\nNo active shift found for {name}");
        }

        Console.WriteLine();
        Console.WriteLine("press any key to exit");
        Console.ReadKey();
    }
    public async Task DeleteShiftAsync(int id)
    {
        await _httpClient.DeleteAsync(_baseUrl + $"api/shifts/{id}");
    }


}
