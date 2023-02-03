using Newtonsoft.Json;
using ShiftLogger.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftLogger.UI
{
    internal class ShiftService
    {
        public static HttpClient ApiClient { get; set; }

        public static void InitializeClient()
        {
            ApiClient = new HttpClient();
            ApiClient.BaseAddress = new Uri("https://localhost:7064");
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static async Task<List<Shift>>? LoadShifts()
        {
            using (HttpResponseMessage response = await ApiClient.GetAsync(ApiClient.BaseAddress + "api/Shifts")) 
            {
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Shift>>(result);
                }
                else
                {
                    Console.WriteLine(response.ReasonPhrase);
                    return null;
                }
            }
            
        }
    }
}
