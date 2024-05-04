using System.Text.Json;

namespace ShiftLoggerConsoleApp;

public class ShiftLoggerService
{
    public async static void GetShift()
    {
        using (HttpClient client = new HttpClient())
        {
            try
            {
                string url = "https://localhost:7256/shiftlogger";
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    var shifts = await JsonSerializer.DeserializeAsync<List<Shift>>(stream);
                    Console.WriteLine("Response:");
                    Console.WriteLine(shifts);
                }
                else
                {
                    Console.WriteLine($"Failed to get data. Status code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}

