using Shifts_Logger.Models;
using System.Net.Http.Json;

namespace ShiftsLoggerUI;

public static class Menu
{
    private static readonly HttpClient _httpClient = new()
    {
        BaseAddress = new Uri("https://localhost:7231/")
    };

    public static async Task ShowMenu()
    {
        while (true)
        {
            Console.WriteLine("\n--- Shifts Logger Menu ---");
            Console.WriteLine("1. View Workers");
            Console.WriteLine("2. Add Worker");
            Console.WriteLine("3. View Shifts");
            Console.WriteLine("4. Add Shift");
            Console.WriteLine("5. Update Shift");
            Console.WriteLine("6. Delete Shift");
            Console.WriteLine("7. Exit");
            Console.Write("Choose an option: ");

            string? choice = Console.ReadLine();
            Console.Clear();

            switch (choice)
            {
                case "1":
                    await ViewWorkers();
                    break;
                case "2":
                    await AddWorker();
                    break;
                case "3":
                    await ViewShifts();
                    break;
                case "4":
                    await AddShift();
                    break;
                case "5":
                    await UpdateShift();
                    break;
                case "6":
                    await DeleteShift();
                    break;
                case "7":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }
        }
    }

    public static async Task ViewWorkers()
    {
        var workers = await _httpClient.GetFromJsonAsync<List<Worker>>("workers");

        if (workers?.Count > 0)
        {
            Console.WriteLine("\nWorkers:");
            foreach (var worker in workers)
                Console.WriteLine($"ID: {worker.Id}, Name: {worker.Name}");
        }
        else
        {
            Console.WriteLine("No workers found.");
        }
    }

    public static async Task AddWorker()
    {
        Console.Write("Enter worker name: ");
        string? name = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Worker name cannot be empty.");
            return;
        }

        var response = await _httpClient.PostAsJsonAsync("workers", new { Name = name });

        if (response.IsSuccessStatusCode)
            Console.WriteLine("Worker added successfully!");
        else
            Console.WriteLine("Failed to add worker.");
    }

    public static async Task ViewShifts()
    {
        var shifts = await _httpClient.GetFromJsonAsync<List<Shift>>("shifts");

        if (shifts?.Count > 0)
        {
            Console.WriteLine("\nShifts:");
            foreach (var shift in shifts)
            {
                Console.WriteLine($"ID: {shift.Id}, Worker ID: {shift.WorkerId}, " +
                                  $"Start: {shift.StartTime:yyyy-MM-dd HH:mm:ss}, End: {shift.EndTime:yyyy-MM-dd HH:mm:ss}, Duration: {shift.Duration}");
            }
        }
        else
        {
            Console.WriteLine("No shifts found.");
        }
    }

    public static async Task AddShift()
    {
        Console.Write("Enter Worker ID: ");
        if (!int.TryParse(Console.ReadLine(), out int workerId))
        {
            Console.WriteLine("Invalid Worker ID.");
            return;
        }

        Console.Write("Enter Shift Start Time (YYYY-MM-DD HH:mm:ss): ");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime startTime))
        {
            Console.WriteLine("Invalid Start Time.");
            return;
        }

        Console.Write("Enter Shift End Time (YYYY-MM-DD HH:mm:ss): ");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime endTime))
        {
            Console.WriteLine("Invalid End Time.");
            return;
        }

        if (startTime >= endTime)
        {
            Console.WriteLine("End Time must be after Start Time.");
            return;
        }

        var response = await _httpClient.PostAsJsonAsync("shifts", new
        {
            WorkerId = workerId,
            StartTime = startTime,
            EndTime = endTime
        });

        if (response.IsSuccessStatusCode)
            Console.WriteLine("Shift added successfully!");
        else
            Console.WriteLine("Failed to add shift.");
    }

    public static async Task UpdateShift()
    {
        Console.Write("Enter Shift ID to update: ");
        if (!int.TryParse(Console.ReadLine(), out int shiftId))
        {
            Console.WriteLine("Invalid Shift ID.");
            return;
        }

        Console.Write("Enter new Shift Start Time (YYYY-MM-DD HH:mm:ss): ");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime startTime))
        {
            Console.WriteLine("Invalid Start Time.");
            return;
        }

        Console.Write("Enter new Shift End Time (YYYY-MM-DD HH:mm:ss): ");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime endTime))
        {
            Console.WriteLine("Invalid End Time.");
            return;
        }

        if (startTime >= endTime)
        {
            Console.WriteLine("End Time must be after Start Time.");
            return;
        }

        var response = await _httpClient.PutAsJsonAsync($"shifts/{shiftId}", new
        {
            StartTime = startTime,
            EndTime = endTime
        });

        if (response.IsSuccessStatusCode)
            Console.WriteLine("Shift updated successfully!");
        else
            Console.WriteLine("Failed to update shift.");
    }

    public static async Task DeleteShift()
    {
        Console.Write("Enter Shift ID to delete: ");
        if (!int.TryParse(Console.ReadLine(), out int shiftId))
        {
            Console.WriteLine("Invalid Shift ID.");
            return;
        }

        var response = await _httpClient.DeleteAsync($"shifts/{shiftId}");

        if (response.IsSuccessStatusCode)
            Console.WriteLine("Shift deleted successfully!");
        else
            Console.WriteLine("Failed to delete shift.");
    }

    public static async Task DeleteWorker()
    {
        Console.Write("Enter Worker ID to delete: ");
        if (!int.TryParse(Console.ReadLine(), out int workerId))
        {
            Console.WriteLine("Invalid Worker ID.");
            return;
        }

        var response = await _httpClient.DeleteAsync($"workers/{workerId}");

        if (response.IsSuccessStatusCode)
            Console.WriteLine("Worker deleted successfully!");
        else
            Console.WriteLine("Failed to delete worker.");
    }
}
