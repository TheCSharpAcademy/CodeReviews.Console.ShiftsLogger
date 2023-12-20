using Newtonsoft.Json;
using ShiftsLogger.Models;
using System.Text;

namespace ShiftsLoggerUI
{
    internal class ShiftsLoggerService
    {
        private readonly ShiftContext _context;

        public ShiftsLoggerService(ShiftContext context)
        {
            _context = context;
        }
        internal async Task AddShift(ShiftModel newShift)
        {
            using (var client = new HttpClient())
            {
                var endpoint = new Uri("https://localhost:7009/api/ShiftModels");
                var json = JsonConvert.SerializeObject(newShift);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var result = await client.PostAsync(endpoint, content);
            }
        }

        internal async Task DeleteShift(int idToDelete)
        {
            using (var client = new HttpClient())
            {
                var endpoint = new Uri($"https://localhost:7009/api/ShiftModels/{idToDelete}");
                var result = await client.DeleteAsync(endpoint);

                if (result.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Shift with ID {idToDelete} deleted successfully.");
                }
                else
                {
                    Console.WriteLine($"Error deleting shift. Status code: {result.StatusCode}");
                }
                Console.ReadLine();
            }
        }

        internal async Task GetShiftById(int idToGet)
        {
            using (var client = new HttpClient())
            {
                var endpoint = new Uri($"https://localhost:7009/api/ShiftModels/{idToGet}");
                var result = client.GetAsync(endpoint).Result;
                var json = result.Content.ReadAsStringAsync().Result;

                var deserializedGet = JsonConvert.DeserializeObject<ShiftModel>(json);

                Console.WriteLine($"Id: {deserializedGet.Id} | StartTime: {deserializedGet.StartTime} | EndTime: {deserializedGet.EndTime} | Worker name: {deserializedGet.WorkerName}");
                Console.ReadLine();
            }
        }

        internal async Task GetShifts()
        {
            Console.Clear();

            using (var client = new HttpClient())
            {
                var endpoint = new Uri("https://localhost:7009/api/ShiftModels");
                var result = client.GetAsync(endpoint).Result;
                var json = result.Content.ReadAsStringAsync().Result;

                var deserializedGet = JsonConvert.DeserializeObject<List<ShiftModel>>(json);

                Console.WriteLine("Shifts" + " |");
                Console.WriteLine("---------------------");

                foreach (var shift in deserializedGet)
                {
                    Console.WriteLine($"Id: {shift.Id} | StartTime: {shift.StartTime} | EndTime: {shift.EndTime} | Worker name: {shift.WorkerName}");
                    Console.WriteLine("---------------------");
                }
            }
            Console.ReadLine();
        }

        internal async Task UpdateShift(int idToUpdate, ShiftModel updatedShift)
        {
            updatedShift.Id = idToUpdate;

            using (var client = new HttpClient())
            {
                var endpoint = new Uri($"https://localhost:7009/api/ShiftModels/{idToUpdate}");
                var json = JsonConvert.SerializeObject(updatedShift);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var result = client.PutAsync(endpoint, content).Result;
            }
        }
    }
}
