using Newtonsoft.Json;
using ShiftsLoggerAPI;

namespace ShiftsLoggerUI
{
    public class Service
    {
        public static HttpClient ApiClient { get; set; }

        public static void InitializeClient()
        {
            ApiClient = new HttpClient();
            ApiClient.BaseAddress = new Uri("https://localhost:7160");
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static async Task AddShift(Shift shift)
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

        public static async Task<List<Shift>>? LoadShifts()
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

        public static async Task DeleteShift(int id)
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

        public static async Task UpdateShift(int id, Shift shift)
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
    }
}
