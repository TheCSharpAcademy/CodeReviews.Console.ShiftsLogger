using ShiftLoggerCarDioLogicsAPI.Model;
using Spectre.Console;
using System.Net.Http.Json;

namespace ShiftLoggerCarDioLogicsUI;

public class Service
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl = "https://localhost:7081";

    public Service(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async void ShiftAddition(string name, DateTime startDate, DateTime endDate)
    {
        var shift = new Shift {Name = name, StartDate = startDate, EndDate = endDate};

        var response = await _httpClient.PostAsJsonAsync(_baseUrl + $"/api/ScaShifts", shift);
        
    }

    public async Task ShiftDeletion(int idToDelete)
    {
        await _httpClient.DeleteAsync(_baseUrl + $"/api/ScaShifts/{idToDelete}");
        Console.Clear ();
    }

    public async void ShiftUpdate(int id, string name, DateTime startDate, DateTime endDate)
    {
        var shift = new Shift {Id = id, Name = name, StartDate = startDate, EndDate = endDate };

        await _httpClient.PutAsJsonAsync(_baseUrl + $"/api/ScaShifts/{shift.Id}", shift);
    }

    public async Task<int> SelectID()
    {
        var shiftsAll = await _httpClient.GetFromJsonAsync<IEnumerable<Shift>>(_baseUrl + "/api/ScaShifts");

        if (shiftsAll == null || !shiftsAll.Any())
        {
            AnsiConsole.WriteLine("No shifts available.");
            return -1;
        }

        var shiftIds = shiftsAll.Select(shift => shift.Id.ToString()).ToArray();

        var selectedId = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select a shift ID:")
                .PageSize(10)
                .MoreChoicesText("[Scroll for more]")
                .AddChoices(shiftIds)
        );

        if (int.TryParse(selectedId, out int idValidated))
        {
            var shiftToDelete = shiftsAll.FirstOrDefault(shift => shift.Id == idValidated);
            if (shiftToDelete != null)
            {
                AnsiConsole.WriteLine($"Shift ID {idValidated} successfully selected.");
                return idValidated;
            }
            else
            {
                AnsiConsole.WriteLine($"Shift ID {idValidated} not found.");
                return -1;
            }
        }
        else
        {
            AnsiConsole.WriteLine("Invalid input. Please select a valid numeric ID.");
            return -1;
        }

        Console.Clear();
    }

    public async Task ShowAllShifts()
    {
          var shiftsAll = await _httpClient.GetFromJsonAsync<IEnumerable<Shift>>(_baseUrl + "/api/ScaShifts");

        var table = new Table();

        // Add headers to the table
        table.AddColumn("ID");
        table.AddColumn("Name");
        table.AddColumn("Start Date");
        table.AddColumn("End Date");
        table.AddColumn("Duration");

        // Add data rows to the table
        foreach (var shift in shiftsAll)
        {
            table.AddRow(
                shift.Id.ToString(),
                shift.Name,
                shift.StartDate.ToString(),
                shift.EndDate.ToString(),
                shift.Duration.ToString()
            );
        }

        AnsiConsole.Render(table);

        Console.WriteLine("Press any Key to continue");
        Console.ReadLine();
    }
}
