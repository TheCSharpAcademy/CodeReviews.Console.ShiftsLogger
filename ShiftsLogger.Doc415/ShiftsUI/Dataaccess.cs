using Newtonsoft.Json;
using ShiftsUI.Models;
using System.Text;
namespace ShiftsUI;

internal class Dataaccess
{
    private string _baseApiConnection;

    public Dataaccess()
    {
        _baseApiConnection = "https://localhost:7201/Shifts";
    }

    internal async Task<List<Shift>> GetShiftsList()
    {
        using HttpClient client = new HttpClient();
        string url = _baseApiConnection;

        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
        try
        {
            HttpResponseMessage response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var shifts = JsonConvert.DeserializeObject<List<Shift>>(content);

                return shifts;
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");
                return null;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return null;
        }
    }

    internal async Task AddShift(string name, string startDate, string endDate, string duration)
    {
        Shift newShift = new()
        {
            Name = name,
            StartTime = startDate,
            EndTime = endDate,
            Duration = duration
        };

        using HttpClient client = new HttpClient();
        string url = _baseApiConnection;

        string newShiftJson = JsonConvert.SerializeObject(newShift);
        HttpContent content = new StringContent(newShiftJson, Encoding.UTF8, "application/json");

        try
        {
            var response = await client.PostAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("New shift recorded. Press Enter to continue...");
                Console.ReadLine();
                Console.Clear();
            }
            else
            {
                Console.WriteLine($"Failed with status code {response.StatusCode}");
                Console.WriteLine("Press Enter to continue...");
                Console.Clear();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"There was an error: {ex.Message}");
            Console.WriteLine("Press Enter to continue...");
            Console.Clear();
        }
    }

    internal async Task<Shift> GetShiftById(int id)
    {
        using HttpClient client = new HttpClient();
        string url = _baseApiConnection + "/" + id;

        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
        try
        {
            HttpResponseMessage response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var shift = JsonConvert.DeserializeObject<Shift>(content);


                return shift;
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");
                return null;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return null;
        }
    }

    internal async Task DeleteShift(int id)
    {
        using HttpClient client = new HttpClient();
        string url = _baseApiConnection + "/" + id;

        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, url);
        try
        {
            HttpResponseMessage response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Record deleted. Press Enter to continue...");
                Console.ReadLine();
                Console.Clear();
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
                Console.Clear();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
            Console.Clear();
        }
    }

    internal async Task UpdateShift(int id, string name, string startTime, string endTime, string duration)
    {
        Shift shiftToUpdate = new()
        {
            Id = id,
            Name = name,
            StartTime = startTime,
            EndTime = endTime,
            Duration = duration
        };

        using HttpClient client = new HttpClient();
        string url = _baseApiConnection + "/" + id;

        string newShiftJson = JsonConvert.SerializeObject(shiftToUpdate);
        HttpContent content = new StringContent(newShiftJson, Encoding.UTF8, "application/json");

        try
        {
            HttpResponseMessage response = await client.PutAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Record updated. Press Enter to continue...");
                Console.ReadLine();
                Console.Clear();
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
                Console.Clear();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
            Console.Clear();
        }

    }
}
