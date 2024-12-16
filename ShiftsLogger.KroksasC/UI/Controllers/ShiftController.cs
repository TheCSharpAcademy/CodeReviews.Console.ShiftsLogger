using System.Net;
using UI.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Http;


namespace UI.Controllers
{
    internal class ShiftController
    {
        private static readonly HttpClient client;

        static ShiftController()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7021/")
            };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public static async Task<Shift> GetShift(string id)
        {
            try
            {
                var shift = await client.GetFromJsonAsync<Shift>($"api/shifts/{id}");
                if (shift == null) 
                {
                    Console.WriteLine("There is no such shift with this id!");
                    Console.WriteLine("Enter any key to continue");
                    Console.ReadLine();
                }
                return shift;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }


        public static async Task<Shift> CreateShift(Shift shift)
        {
            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync("api/shifts", shift);

                if (response.IsSuccessStatusCode)
                {
                    var createdShift = await response.Content.ReadFromJsonAsync<Shift>();
                    return createdShift;
                }
                else
                {
                    Console.WriteLine($"Failed to create shift. Status code: {response.StatusCode}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating shift: {ex.Message}");
                return null;
            }
        }

        public static async Task<HttpStatusCode> DeleteShift(int shiftId)
        {
            try
            {
                HttpResponseMessage response = await client.DeleteAsync($"api/shifts/{shiftId}");
                return response.StatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return HttpStatusCode.InternalServerError;
            }
        }
        public static async Task<List<Shift>> GetShifts()
        {
            try
            {
                // Fetch a list of shifts from the API
                var shifts = await client.GetFromJsonAsync<List<Shift>>("api/shifts");

                if (shifts != null && shifts.Count > 0)
                {
                    return shifts;
                }
                else
                {
                    Console.WriteLine("No shifts found.");
                    return null;
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP Request Error: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching shifts: {ex.Message}");
                return null;
            }
        }
        public static async Task<Shift> UpdateShift(Shift shift)
        {
            try
            {
                HttpResponseMessage response = await client.PutAsJsonAsync($"api/shifts/{shift.Id}", shift);

                if (response.IsSuccessStatusCode)
                {
                    var createdShift = await response.Content.ReadFromJsonAsync<Shift>();
                    return createdShift;
                }
                else
                {
                    Console.WriteLine($"Failed to update shift. Status code: {response.StatusCode}");
                    Console.ReadLine();
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating shift: {ex.Message}");
                return null;
            }
        }
    }
}
