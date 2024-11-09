using ShiftLogger.ShiftTrack.Views;
using ShiftLogger.ShiftTrack.Models;
using System.Text.Json;
using System.Net.Http.Headers;
using System.Text;

namespace ShiftLogger.ShiftTrack.Services;

internal static class WorkerService
{
    internal static async Task<bool> DisplayWorkersAsync(HttpClient client)
    {
        try
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            string json = await client.GetStringAsync($"https://localhost:7134/api/Workers");
            List<Worker>? workers = JsonSerializer.Deserialize<List<Worker>>(json, options);
            if (workers.Count == 0)
            {
                Console.WriteLine("No workers exist. Press Enter to continue");
                return false;
            }
            else
                Display.DisplayWorkers(workers, new string[] { "Worker ID", "Name", "Email ID" });
            Console.WriteLine("Press Enter to continue");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return false;
        }
        finally { Console.ReadLine(); }
    }

    internal static async Task PostWorkerAsync(HttpClient client, Worker worker)
    {
        try
        {
            var url = "https://localhost:7134/api/Workers";
            var jsonContent = new StringContent(JsonSerializer.Serialize(worker), Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
            var response = await client.PostAsync(url, jsonContent);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Worker added successfully: {responseContent}. Press Enter to continue.");
            }
            else
                Console.WriteLine($"Failed to add worker. Status code: {response.StatusCode}.  Press Enter to continue.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
        finally { Console.ReadLine(); }
    }

    internal static async Task DeleteWorkerAsync(HttpClient client, int workerId)
    {
        try
        {
            var url = $"https://localhost:7134/api/Workers/{workerId}";
            var response = await client.DeleteAsync(url);
            if (response.IsSuccessStatusCode)
                Console.WriteLine($"Worker with ID {workerId} deleted successfully. Press Enter to continue");
            else
                Console.WriteLine($"Failed to delete worker. Status code: {response.StatusCode}. Press Enter to continue");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
        finally { Console.ReadLine(); }
    }

    internal static async Task<int> GetWorkerIdForShift(HttpClient client, int shiftId)
    {
        try
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var url = $"https://localhost:7134/api/Shifts/{shiftId}";
            var response = await client.GetStringAsync(url);
            Shift? shift = JsonSerializer.Deserialize<Shift>(response, options);
            if (shift != null)
            {
                return shift.WorkerId;
            }
            else
            {
                Console.WriteLine("Shift not found.");
                return -1;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return -1;
        }
    }

    internal static async Task PutWorkersAsync(HttpClient client, Worker worker)
    {
        try
        {
            var url = $"https://localhost:7134/api/Workers/{worker.WorkerId}";
            var jsonContent = new StringContent(JsonSerializer.Serialize(worker), Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
            var response = await client.PutAsync(url, jsonContent);
            if (response.IsSuccessStatusCode)
                Console.WriteLine("Worker edited successfully");
            else
                Console.WriteLine($"Failed to edit worker. Status code: {response.StatusCode}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
        finally { Console.ReadLine(); }
    }

    internal static async Task<bool> CheckWorkerExistsAsync(HttpClient client, int workerId)
    {
        try
        {
            var url = $"https://localhost:7134/api/Workers/{workerId}";
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