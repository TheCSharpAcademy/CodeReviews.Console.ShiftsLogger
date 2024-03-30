using Newtonsoft.Json;
using ShiftLoggerClient.Models;
using System.Text;

namespace ShiftLoggerClient.ClientControllers;

internal class PunchCardClientController
{
    internal static ShiftDTO CreateShift(ShiftDTO newShift)
    {


        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri("https://localhost:7196");
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        try
        {
            string jsonEmployee = JsonConvert.SerializeObject(newShift);
            StringContent content = new StringContent(jsonEmployee, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync("api/shift", content).Result;
            response.EnsureSuccessStatusCode();

            string jsonResponse = response.Content.ReadAsStringAsync().Result;
            ShiftDTO addedShift = JsonConvert.DeserializeObject<ShiftDTO>(jsonResponse);

            return addedShift;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"HTTP request failed: {ex.Message}");
            if (ex.InnerException != null)
            {
                Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
            }
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            throw;
        }
    }

    public static List<ShiftDTO> GetAllShiftsController()
    {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7196");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                HttpResponseMessage response = client.GetAsync($"api/shift").Result;
                response.EnsureSuccessStatusCode();

                string jsonContent = response.Content.ReadAsStringAsync().Result;
                var allShifts = JsonConvert.DeserializeObject<List<ShiftDTO>>(jsonContent);
                return allShifts;
            }

            catch (HttpRequestException ex)
            {

                Console.WriteLine($"HTTP request failed: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
                return new List<ShiftDTO>();
            }

            catch (Exception ex)
            {

                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        
    }

    internal static ShiftDTO GetOpenShift(EmployeeDTO employee)
    {
        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri("https://localhost:7196");
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        try
        {
            HttpResponseMessage response = client.GetAsync($"api/shift/byemployee/{employee.Id}/true").Result;
            response.EnsureSuccessStatusCode();

            string jsonContent = response.Content.ReadAsStringAsync().Result;
            var shift = JsonConvert.DeserializeObject<ShiftDTO>(jsonContent);
            return shift;
        }

        catch (HttpRequestException ex)
        {

            Console.WriteLine($"HTTP request failed: {ex.Message}");
            if (ex.InnerException != null)
            {
                Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
            }
            return new ShiftDTO();
        }

        catch (Exception ex)
        {

            Console.WriteLine($"An error occurred: {ex.Message}");
            throw;
        }
    }

    internal static List<ShiftDTO> GetOpenShiftsController()
    {
        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri("https://localhost:7196");
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        try
        {
            HttpResponseMessage response = client.GetAsync($"api/shift/open").Result;
            response.EnsureSuccessStatusCode();

            string jsonContent = response.Content.ReadAsStringAsync().Result;
            var openShifts = JsonConvert.DeserializeObject<List<ShiftDTO>>(jsonContent);
            return openShifts;
        }

        catch (HttpRequestException ex)
        {

            Console.WriteLine($"HTTP request failed: {ex.Message}");
            if (ex.InnerException != null)
            {
                Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
            }
            return new List<ShiftDTO>();
        }

        catch (Exception ex)
        {

            Console.WriteLine($"An error occurred: {ex.Message}");
            throw;
        }

    }

    internal static ShiftDTO GetShiftByID(int shiftId)
    {
        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri("https://localhost:7196");
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        try
        {
            HttpResponseMessage response = client.GetAsync($"api/shift/id?id={shiftId}").Result;
            response.EnsureSuccessStatusCode();

            string jsonContent = response.Content.ReadAsStringAsync().Result;
            var shift = JsonConvert.DeserializeObject<ShiftDTO>(jsonContent);
            return shift;
        }

        catch (HttpRequestException ex)
        {

            Console.WriteLine($"HTTP request failed: {ex.Message}");
            if (ex.InnerException != null)
            {
                Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
            }
            return new ShiftDTO();
        }

        catch (Exception ex)
        {

            Console.WriteLine($"An error occurred: {ex.Message}");
            throw;
        }
    }

    internal static List<ShiftDTO> GetShiftByEmployeeId(int employeeId)
    {
        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri("https://localhost:7196");
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        try
        {
            HttpResponseMessage response = client.GetAsync($"api/shift/byemployee/{employeeId}").Result;
            response.EnsureSuccessStatusCode();

            string jsonContent = response.Content.ReadAsStringAsync().Result;
            var openShifts = JsonConvert.DeserializeObject<List<ShiftDTO>>(jsonContent);
            return openShifts;
        }

        catch (HttpRequestException ex)
        {

            Console.WriteLine($"HTTP request failed: {ex.Message}");
            if (ex.InnerException != null)
            {
                Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
            }
            return new List<ShiftDTO>();
        }

        catch (Exception ex)
        {

            Console.WriteLine($"An error occurred: {ex.Message}");
            throw;
        }
    }

    internal static ShiftDTO UpdateShiftController(ShiftDTO shiftDTO)
    {
        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri("https://localhost:7196");
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        try
        {
            string jsonShift = JsonConvert.SerializeObject(shiftDTO);
            StringContent content = new StringContent(jsonShift, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PutAsync($"api/shift/{shiftDTO.Id}", content).Result;
            response.EnsureSuccessStatusCode();

            // Deserialize the response content to EmployeeDTO
            string jsonResponse = response.Content.ReadAsStringAsync().Result;
            ShiftDTO closedShift = JsonConvert.DeserializeObject<ShiftDTO>(jsonResponse);

            return shiftDTO;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"HTTP request failed: {ex.Message}");
            if (ex.InnerException != null)
            {
                Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
            }
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            throw;
        }
    }

    internal static bool DeleteShiftController(ShiftDTO shiftToDelete)
    {

        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri("https://localhost:7196");
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        try
        {
            string jsonEmployee = JsonConvert.SerializeObject(shiftToDelete);
            StringContent content = new StringContent(jsonEmployee, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.DeleteAsync($"api/shift/{shiftToDelete.Id}").Result;
            response.EnsureSuccessStatusCode();

            return true;
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"HTTP request failed: {ex.Message}");
            if (ex.InnerException != null)
            {
                Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
            }
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            throw;
        }
    }
}

