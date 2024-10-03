using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using ShiftLogger.ShiftTrack.Views;
using ShiftLogger.ShiftTrack.Models;

namespace ShiftLogger.ShiftTrack;

internal class App
{
    public async Task InitializeClientAsync()
    {
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        await GetSelectionAsync(client);
    }

    private async Task GetSelectionAsync(HttpClient client)
    {
        string selection = Display.GetSelection("Welcome to Shift Logger", new []{ "View Workers/Shifts", "Create a Worker/Shift", "Edit Worker/Shift", "Delete Worker/Shift", "Exit Application"});
        while(selection != "Exit Application")
        {
            if (selection == "View Workers/Shifts")
            {
                selection = Display.GetSelection("What do you wish to do", new[] { "View Workers", "View shifts", "Go to Main Menu" });
                if (selection == "View Workers")
                    await DisplayWorkersAsync(client);
                else if (selection == "View shifts")
                    await DisplayShiftsAsync(client);
            }
            else if (selection == "Create a Worker/Shift")
            {
                selection = Display.GetSelection("What do you wish to do", new[] { "Create a Worker", "Create a shift", "Go to Main Menu"});
                if (selection == "Create a Worker")
                {
                    Worker worker = Display.GetWorkerDetails();
                    await PostWorkerAsync(client, worker);
                }
                else if (selection == "Create a shift")
                {
                    await DisplayWorkersAsync(client);
                    Shift shift = Display.GetShiftDetails();
                    await PostShiftAsync(client, shift);
                }
            }
            else if (selection == "Edit Worker/Shift")
            {
                selection = Display.GetSelection("What do you wish to do", new[] { "Edit A Worker", "Edit a shift", "Go to Main Menu"});
            }
            else if (selection == "Delete Worker/Shift")
            {
                selection = Display.GetSelection("What do you wish to do", new[] { "Delete A Worker", "Delete a shift", "Go to Main Menu" });
            }
            Console.Clear();
            selection = Display.GetSelection("Welcome to Shift Logger", new[] { "View Workers/Shifts", "Create a Worker/Shift", "Edit Worker/Shift", "Delete Worker/Shift", "Exit Application" });
        }
        
    }
  
    private async Task DisplayWorkersAsync(HttpClient client)
    {
        try
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            string json = await client.GetStringAsync($"https://localhost:7134/api/Workers");
            List<Worker> ?workers = JsonSerializer.Deserialize<List<Worker>>(json, options);
            Display.DisplayWorkers(workers, new string[] { "Worker ID", "Name", "Email ID" });
            Console.WriteLine("Press any key to continue");
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Request error: {ex.Message}");
        }
        catch (TaskCanceledException ex)
        {
            Console.WriteLine("Request timed out.");
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

    private async Task DisplayShiftsAsync(HttpClient client)
    {
        try
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            string json = await client.GetStringAsync($"https://localhost:7134/api/Shifts");
            List<Shift>? shift = JsonSerializer.Deserialize<List<Shift>>(json, options);
            Display.DisplayShifts(shift, new string[] { "Date", "Start Time", "End Time", "Duration", "WorkerId" });
            Console.WriteLine("Press any key to continue");
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Request error: {ex.Message}");
        }
        catch (TaskCanceledException ex)
        {
            Console.WriteLine("Request timed out.");
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

    private async Task PostWorkerAsync(HttpClient client, Worker worker)
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
                Console.WriteLine($"Worker added successfully: {responseContent}");
            }
            else
            {
                Console.WriteLine($"Failed to add worker. Status code: {response.StatusCode}");
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Request error: {ex.Message}");
        }
        catch (TaskCanceledException ex)
        {
            Console.WriteLine("Request timed out.");
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

    private async Task PostShiftAsync(HttpClient client, Shift shift)
    {
        try
        {
            
            var url = "https://localhost:7134/api/Shifts";
            var jsonContent = new StringContent(JsonSerializer.Serialize(shift), Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
            var response = await client.PostAsync(url, jsonContent);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Shift added successfully: {responseContent}");
            }
            else
            {
                Console.WriteLine($"Failed to add shift. Status code: {response.StatusCode}");
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Request error: {ex.Message}");
        }
        catch (TaskCanceledException ex)
        {
            Console.WriteLine($"Request timed out. {ex.Message}");
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
}