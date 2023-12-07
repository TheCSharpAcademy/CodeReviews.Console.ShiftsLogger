using Newtonsoft.Json;
using ShiftsLogger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftsLoggerUI
{
    internal class APICalls
    {
        internal static void PrintShifts()
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
                    TimeSpan shiftDuration = shift.EndTime - shift.StartTime;
                    Console.WriteLine($"Id: {shift.Id} | StartTime: {shift.StartTime} | EndTime: {shift.EndTime} | Duration: {shiftDuration}| Worker name: {shift.WorkerName}");
                    Console.WriteLine("---------------------");
                }
            }
        }
    }
}
