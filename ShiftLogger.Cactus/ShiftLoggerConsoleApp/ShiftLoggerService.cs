using System.Text.Json;

namespace ShiftLoggerConsoleApp;

public class ShiftLoggerService
{
    public async static Task<List<Shift>> GetShifts()
    {
        using (HttpClient client = new HttpClient())
        {
            List<Shift> shifts = new List<Shift>();
            try
            {
                string url = "https://localhost:7256/shiftlogger";
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync();
                    shifts = await JsonSerializer.DeserializeAsync<List<Shift>>(stream);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return shifts;
        }
    }
}

