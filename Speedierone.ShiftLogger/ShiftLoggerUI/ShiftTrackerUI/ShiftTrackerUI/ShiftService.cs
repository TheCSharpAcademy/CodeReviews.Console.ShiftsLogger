using Newtonsoft.Json;
using RestSharp;
using ShiftTrackerUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftTrackerUI
{
    internal class ShiftService
    {
        public static async Task GetShifts()
        {
            Console.Clear();

            string apiEndPoint = "https://localhost:7217/api/ShiftTimes";

            List<Shift> resultList = new List<Shift>();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(apiEndPoint);

                    if (response.IsSuccessStatusCode)
                    {
                        string data = await response.Content.ReadAsStringAsync();

                        resultList = JsonConvert.DeserializeObject<List<Shift>>(data);

                        TableVisualisation.ShowTable(resultList, "Shifts");
                    }
                    else
                    {
                        Console.WriteLine($"Error: {response.StatusCode}");
                    }
                }
                Console.ReadLine();
            }
            catch (Exception ex) { Console.WriteLine(ex); Console.ReadLine(); }
        }
        public static async Task PostShift(string name, DateTime startDate, DateTime endTime, DateTime duration)
        {
            Console.Clear();

            await GetShifts();          
        }
    }
}
