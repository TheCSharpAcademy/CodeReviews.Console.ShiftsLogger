using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ShiftsLogger.frockett.UI.Dtos;

namespace ShiftsLogger.frockett.UI;

public class ApiService
{
    private readonly HttpClient httpClient;
    private readonly string baseUri;

    public ApiService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    internal async Task<List<ShiftDto>> GetAllShifts()
    {
        string requestUrl = "shifts";

        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestUrl);
        try
        {
            HttpResponseMessage response = await httpClient.SendAsync(request);

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
        string url = baseUri;

        string newShiftJson = JsonConvert.SerializeObject(newShift);
        HttpContent content = new StringContent(newShiftJson, Encoding.UTF8, "application/json");

        try
        {
            var response = await httpClient.PostAsync(url, content);
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
        //using HttpClient client = new HttpClient();
        string url = baseUri + "/" + id;

        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
        try
        {
            HttpResponseMessage response = await httpClient.SendAsync(request);

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
        //using HttpClient client = new HttpClient();
        string url = baseUri + "/" + id;

        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, url);
        try
        {
            HttpResponseMessage response = await httpClient.SendAsync(request);

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

        //using HttpClient client = new HttpClient();
        string url = baseUri + "/" + updateShift.Id;

        string newShiftJson = JsonConvert.SerializeObject(updateShift);
        HttpContent content = new StringContent(newShiftJson, Encoding.UTF8, "application/json");

        try
        {
            HttpResponseMessage response = await httpClient.PutAsync(url, content);

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

    internal async Task<List<ShiftDto>>? GetShiftsByEmployeeId(int employeeId)
    {
        string requestUrl = $"employee/{employeeId}";

        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestUrl);

        try
        {
            HttpResponseMessage response = await httpClient.SendAsync(request);

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
        catch(Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return null;
        }
    }

    internal async Task<List<EmployeeDto>>? GetListOfEmployees()
    {
        string requestUrl = "employee";

        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestUrl);

        try
        {
            HttpResponseMessage response = await httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var employees = JsonConvert.DeserializeObject<List<EmployeeDto>>(content);

                return employees;
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
}
