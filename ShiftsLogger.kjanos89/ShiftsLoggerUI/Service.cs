using Newtonsoft.Json;
using ShiftsLoggerAPI;

namespace ShiftsLoggerUI
{
    public class Service
    {
        public static HttpClient ApiClient { get; set; }

        public static void InitializeClient()
        {
            try
            {
                ApiClient = new HttpClient();
                ApiClient.BaseAddress = new Uri("https://localhost:7160");
                ApiClient.DefaultRequestHeaders.Accept.Clear();
                ApiClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static async Task AddShift(Shift shift)
        {
            try
            {
                using HttpResponseMessage answer = await ApiClient.PostAsJsonAsync(ApiClient.BaseAddress + "api/Shifts", shift);
                if (answer.IsSuccessStatusCode)
                {
                    Console.WriteLine("New shift record added successfully.");
                }
                else
                {
                    Console.WriteLine(answer.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Task.Delay(1600);
            }
        }

        public static async Task<List<Shift>>? LoadShifts()
        {
            try
            {
                using HttpResponseMessage answer = await ApiClient.GetAsync(ApiClient.BaseAddress + "api/Shifts");
                if (answer.IsSuccessStatusCode)
                {
                    string result = await answer.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Shift>>(result);
                }
                else
                {
                    Console.WriteLine(answer.ReasonPhrase);
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Task.Delay(1600);
                return null;
            }
        }

        public static async Task DeleteShift(int id)
        {
            try
            {
                using HttpResponseMessage answer = await ApiClient.DeleteAsync(ApiClient.BaseAddress + $"api/Shifts/{id}");
                if (answer.IsSuccessStatusCode)
                {
                    Console.WriteLine("Shift record removed successfully.");
                }
                else
                {
                    Console.WriteLine(answer.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Task.Delay(1600);
            }
        }

        public static async Task UpdateShift(int id, Shift shift)
        {
            try
            {
                using HttpResponseMessage answer = await ApiClient.PutAsJsonAsync(ApiClient.BaseAddress + $"api/Shifts/{id}", shift);
                if (answer.IsSuccessStatusCode)
                {
                    Console.WriteLine("Record update successful.");
                }
                else
                {
                    Console.WriteLine(answer.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error {ex.Message}");
                Task.Delay(1600);
            }
        }
    }
}