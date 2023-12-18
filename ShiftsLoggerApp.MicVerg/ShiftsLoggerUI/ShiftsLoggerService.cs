using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ShiftsLogger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        internal async Task DeleteShift()
        {
            throw new NotImplementedException();
        }

        internal async Task GetShiftById()
        {
            throw new NotImplementedException();
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

        internal async Task UpdateShift()
        {
            throw new NotImplementedException();
        }
    }
}
