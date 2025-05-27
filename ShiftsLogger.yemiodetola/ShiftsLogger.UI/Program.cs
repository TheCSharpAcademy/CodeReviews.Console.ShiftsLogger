using ShiftsLogger.UI.Models;
using ShiftsLogger.UI.Services;


class Program
{
  private static ApiService _apiService = new ApiService();

  static async Task Main()
  {
    Console.WriteLine("---------- Shifts Record ----------");
    Console.WriteLine();

    bool running = true;

    while (running)
    {
      ShowMenu();
      var choice = Console.ReadLine();

      switch (choice)
      {
        case "1":
          await ViewAllShifts();
          break;
        case "2":
          await ViewShiftById();
          break;
        case "3":
          await CreateShift();
          break;
        case "4":
          await UpdateShift();
          break;
        case "5":
          await DeleteShift();
          break;
        case "0":
          running = false;
          break;
        default:
          Console.WriteLine("Invalid option. Please try again.");
          break;
      }

      if (running)
      {
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
        Console.Clear();
      }
    }
    _apiService.Dispose();
    Console.WriteLine("Goodbye!");
  }

  static void ShowMenu()
  {
    Console.WriteLine("Please select an option:");
    Console.WriteLine("1. View all shifts");
    Console.WriteLine("2. View shift by ID");
    Console.WriteLine("3. Create new shift");
    Console.WriteLine("4. Update shift");
    Console.WriteLine("5. Delete shift");
    Console.WriteLine("0. Exit");
    Console.Write("Your choice: ");
  }

  static async Task ViewAllShifts()
  {
    Console.WriteLine("\n-----Shifts Record -----");
    var shifts = await _apiService.GetAllShiftsAsync();
    if (shifts.Count == 0)
    {
      Console.WriteLine("No shifts found.");
      return;
    }

    Console.WriteLine($"{"ID",-5} {"Name",-20} {"Start At",-20} {"Finished At",-20} {"Duration",-10}");

    foreach (var shift in shifts)
    {
      Console.WriteLine($"{shift.Id,-5} {shift.Name,-20} {shift.StartedAt,-20:MM/dd/yyyy HH:mm} {shift.FinishedAt,-20:MM/dd/yyyy HH:mm} {shift.Duration.ToString(@"hh\:mm"),-10}");
    }
  }


  static async Task ViewShiftById()
  {
    Console.Write("\nEnter shift ID: ");
    if (!int.TryParse(Console.ReadLine(), out int id))
    {
      Console.WriteLine("Invalid ID format.");
      return;
    }

    var shift = await _apiService.GetShiftByIdAsync(id);
    if (shift == null)
    {
      Console.WriteLine("Shift not found.");
      return;
    }

    Console.WriteLine("\n=== Shift Details ===");
    Console.WriteLine($"ID: {shift.Id}");
    Console.WriteLine($"Worker Name: {shift.Name}");
    Console.WriteLine($"Start Time: {shift.StartedAt:MM/dd/yyyy HH:mm}");
    Console.WriteLine($"End Time: {shift.FinishedAt:MM/dd/yyyy HH:mm}");
    Console.WriteLine($"Duration: {shift.Duration:hh\\:mm}");
  }


  static async Task CreateShift()
  {
    Console.WriteLine("\n-------- Create New Shift --------");

    var shift = new Shift();

    Console.Write("Worker's Name: ");
    shift.Name = Console.ReadLine()?.Trim() ?? "";
    if (string.IsNullOrEmpty(shift.Name))
    {
      Console.WriteLine("Worker's name is required.");
      return;
    }

    Console.Write("Start Time (MM/dd/yyyy HH:mm): ");
    if (!DateTime.TryParse(Console.ReadLine(), out DateTime startTime))
    {
      Console.WriteLine("Invalid date format.");
      return;
    }
    shift.StartedAt = startTime;

    Console.Write("End Time (MM/dd/yyyy HH:mm): ");
    if (!DateTime.TryParse(Console.ReadLine(), out DateTime finishedAt))
    {
      Console.WriteLine("Invalid date format.");
      return;
    }
    shift.FinishedAt = finishedAt;

    if (shift.FinishedAt <= shift.StartedAt)
    {
      Console.WriteLine("End time must be after start time.");
      return;
    }

    var success = await _apiService.CreateShiftAsync(shift);
    if (success)
    {
      Console.WriteLine("Shift created successfully!");
    }
    else
    {
      Console.WriteLine("Failed to create shift.");
    }
  }

  static async Task UpdateShift()
  {
    Console.Write("\nEnter shift ID to update: ");
    if (!int.TryParse(Console.ReadLine(), out int id))
    {
      Console.WriteLine("Invalid ID format.");
      return;
    }

    var existingShift = await _apiService.GetShiftByIdAsync(id);
    if (existingShift == null)
    {
      Console.WriteLine("Shift not found.");
      return;
    }

    Console.WriteLine("\n------- Update Shift -------");
    Console.WriteLine("Press Enter to keep current value, or enter new value:");

    var updatedShift = new Shift { Id = id };

    Console.Write($"Worker Name [{existingShift.Name}]: ");
    var name = Console.ReadLine()?.Trim();
    updatedShift.Name = string.IsNullOrEmpty(name) ? existingShift.Name : name;


    Console.Write($"Start Time [{existingShift.StartedAt:MM/dd/yyyy HH:mm}]: ");
    var startTimeInput = Console.ReadLine()?.Trim();
    if (string.IsNullOrEmpty(startTimeInput))
    {
      updatedShift.StartedAt = existingShift.StartedAt;
    }
    else if (!DateTime.TryParse(startTimeInput, out DateTime startTime))
    {
      Console.WriteLine("Invalid date format.");
      return;
    }
    else
    {
      updatedShift.StartedAt = startTime;
    }

    Console.Write($"End Time [{existingShift.FinishedAt:MM/dd/yyyy HH:mm}]: ");
    var endTimeInput = Console.ReadLine()?.Trim();
    if (string.IsNullOrEmpty(endTimeInput))
    {
      updatedShift.FinishedAt = existingShift.FinishedAt;
    }
    else if (!DateTime.TryParse(endTimeInput, out DateTime finishedAt))
    {
      Console.WriteLine("Invalid date format.");
      return;
    }
    else
    {
      updatedShift.FinishedAt = finishedAt;
    }

    if (updatedShift.FinishedAt <= updatedShift.StartedAt)
    {
      Console.WriteLine("End time must be after start time.");
      return;
    }

    var success = await _apiService.UpdateShiftAsync(id, updatedShift);
    if (success)
    {
      Console.WriteLine("Shift updated successfully!");
    }
    else
    {
      Console.WriteLine("Failed to update shift.");
    }
  }

  static async Task DeleteShift()
  {
    Console.Write("\nEnter shift ID to delete: ");
    if (!int.TryParse(Console.ReadLine(), out int id))
    {
      Console.WriteLine("Invalid ID format.");
      return;
    }

    // First, show the shift details
    var shift = await _apiService.GetShiftByIdAsync(id);
    if (shift == null)
    {
      Console.WriteLine("Shift not found.");
      return;
    }

    Console.WriteLine("\n------ Shift to Delete -------");
    Console.WriteLine($"ID: {shift.Id}");
    Console.WriteLine($"Worker: {shift.Name}");
    Console.WriteLine($"Time: {shift.StartedAt:MM/dd/yyyy HH:mm} - {shift.FinishedAt:MM/dd/yyyy HH:mm}");

    Console.Write("\nAre you sure you want to delete this shift? (y/N): ");
    var confirmation = Console.ReadLine()?.Trim().ToLower();

    if (confirmation == "y" || confirmation == "yes")
    {
      var success = await _apiService.DeleteShiftAsync(id);
      if (success)
      {
        Console.WriteLine("Shift deleted successfully!");
      }
      else
      {
        Console.WriteLine("Failed to delete shift.");
      }
    }
    else
    {
      Console.WriteLine("Delete cancelled.");
    }
  }

}
