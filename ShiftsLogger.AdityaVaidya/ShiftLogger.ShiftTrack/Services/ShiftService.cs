using ShiftLogger.ShiftTrack.Views;
using ShiftLogger.ShiftTrack.Models;
using System.Text.Json;
using System.Net.Http.Headers;
using System.Text;

namespace ShiftLogger.ShiftTrack.Services;

internal static class ShiftService
{
    internal static async Task<bool> DisplayShiftsAsync(HttpClient client)
    {
        try
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            string shiftJson = await client.GetStringAsync($"https://localhost:7134/api/Shifts");
            List<Shift>? shifts = JsonSerializer.Deserialize<List<Shift>>(shiftJson, options);
            if (shifts.Count == 0)
            {
                Console.WriteLine("No shifts exist. Press Enter to continue.");
                return false;
            }
            else
            {
                string workerJson = await client.GetStringAsync($"https://localhost:7134/api/Workers");
                List<Worker>? workers = JsonSerializer.Deserialize<List<Worker>>(workerJson, options);
                var shiftDetails = from shift in shifts
                                   join worker in workers on shift.WorkerId equals worker.WorkerId
                                   select new
                                   {
                                       shift.ShiftId,
                                       WorkerName = worker.Name,
                                       shift.Date,
                                       shift.StartTime,
                                       shift.EndTime,
                                       shift.Duration
                                   };
                Display.DisplayShifts(shiftDetails.ToList(), new string[] { "ShiftID", "Worker Name", "Date", "Start Time", "End Time", "Duration" });
                Console.WriteLine("Press Enter to continue");
                return true;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return false;
        }
        finally
        {
            Console.ReadLine();
        }
    }

    internal static async Task PostShiftAsync(HttpClient client, Shift shift)
    {
        try
        {
            var url = "https://localhost:7134/api/Shifts";
            var jsonContent = new StringContent(JsonSerializer.Serialize(shift), Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
            var response = await client.PostAsync(url, jsonContent);
            if (response.IsSuccessStatusCode)
                Console.WriteLine("Shift added successfully.Press Enter to continue");
            else
                Console.WriteLine($"Failed to add shift. Status code: {response.StatusCode}. Press Enter to continue");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
        finally
        {
            Console.ReadLine();
        }
    }

    internal static async Task DeleteShiftAsync(HttpClient client, int shiftId)
    {
        try
        {
            var url = $"https://localhost:7134/api/Shifts/{shiftId}";
            var response = await client.DeleteAsync(url);
            if (response.IsSuccessStatusCode)
                Console.WriteLine($"Worker with ID {shiftId} deleted successfully.");
            else
                Console.WriteLine($"Failed to delete worker. Status code: {response.StatusCode}. Press Enter to continue");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
        finally
        {
            Console.WriteLine("Press Enter to continue.");
            Console.ReadLine();
        }
    }

    internal static async Task PutShiftAsync(HttpClient client, Shift shift)
    {
        try
        {
            var url = $"https://localhost:7134/api/Shifts/{shift.ShiftId}";
            var jsonContent = new StringContent(JsonSerializer.Serialize(shift), Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
            var response = await client.PutAsync(url, jsonContent);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Shift edited successfully: {responseContent}");
            }
            else
            {
                Console.WriteLine($"Failed to edit shift. Status code: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
        finally
        {
            Console.ReadLine();
        }
    }

    internal static async Task<bool> CheckShiftExistsAsync(HttpClient client, int shiftId)
    {
        try
        {
            var url = $"https://localhost:7134/api/Shifts/{shiftId}";
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return false;
            }
            else
            {
                Console.WriteLine($"Unexpected status code: {response.StatusCode}");
                return false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return false;
        }
    }
}