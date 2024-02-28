using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ShiftsLogger.frockett.UI.Dtos;

namespace ShiftsLogger.frockett.UI;

internal class ApiService
{
    private readonly HttpClient httpClient;
    private readonly string baseUri;

    public ApiService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
        baseUri = "https://localhost:7127/";
    }

    internal async Task<List<ShiftDto>> GetShiftsList()
    {
        using HttpClient client = new HttpClient();
        string url = baseUri;

        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
        try
        {
            HttpResponseMessage response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var shifts = JsonConvert.DeserializeObject<List<ShiftDto>>(content);

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

    internal async Task AddShift(ShiftCreateDto newShift)
    {
        using HttpClient client = new HttpClient();
        string url = baseUri;

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

    internal async Task<ShiftDto> GetEmployeeShifts(int id)
    {
        using HttpClient client = new HttpClient();
        string url = baseUri + "/" + id;

        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
        try
        {
            HttpResponseMessage response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var shift = JsonConvert.DeserializeObject<ShiftDto>(content);


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
        string url = baseUri + "/" + id;

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

    internal async Task UpdateShift(ShiftDto updateShift)
    {

        using HttpClient client = new HttpClient();
        string url = baseUri + "/" + updateShift.Id;

        string newShiftJson = JsonConvert.SerializeObject(updateShift);
        HttpContent content = new StringContent(newShiftJson, Encoding.UTF8, "application/json");

        try
        {
            HttpResponseMessage response = await client.PutAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Update Successful. Press Enter to continue...");
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
