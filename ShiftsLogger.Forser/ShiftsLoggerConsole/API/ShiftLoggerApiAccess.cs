using System.Net.Http.Json;

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
    }
}