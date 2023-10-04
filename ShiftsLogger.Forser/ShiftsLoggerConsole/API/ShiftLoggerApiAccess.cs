using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace ShiftsLoggerConsole.API
{
    internal class ShiftLoggerApiAccess : IShiftsLoggerApiAccess
    {
        private readonly HttpClient _apiClient;

        public ShiftLoggerApiAccess(HttpClient apiClient)
        {
            _apiClient = apiClient;
        }
        public async Task<IEnumerable<Shift>> GetShifts()
        {
            var response = await _apiClient.GetFromJsonAsync<List<Shift>>("");

            return response;
        }
        public async Task<bool> PostShift(ShiftDto shift)
        {
            var json = JsonSerializer.Serialize(shift);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await _apiClient.PostAsync("", content))
            {
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public async Task<bool> DeleteShift(int selectedShift)
        {
            using (HttpResponseMessage response = await _apiClient.DeleteAsync($"{selectedShift}"))
            {
                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }
            }

            return true;
        }
        public async Task<Shift> GetShift(int id)
        {
            var response = await _apiClient.GetFromJsonAsync<Shift>($"{id}");

            return response;
        }
        public async Task<bool> UpdateShift(ShiftDto newShift)
        {
            using (HttpResponseMessage response = await _apiClient.PutAsJsonAsync($"{newShift.Id}", newShift))
            {
                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }
            }
            return true;
        }
    }
}