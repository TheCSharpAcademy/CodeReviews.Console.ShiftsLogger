using Newtonsoft.Json;
using ShiftTrackerUI.Models;
using System.Text;
using System.Configuration;

namespace ShiftTrackerUI
{
    internal class ShiftService
    {
        public static async Task GetShifts()
        {
            Console.Clear();

            string apiEndPoint = ConfigurationManager.AppSettings.Get("apiAddress");

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
        public static async Task PostShift(string name, string startDate, string startTime, string endTime, string duration)
        {
            try
            {
                var apiUrl = ConfigurationManager.AppSettings.Get("apiAddress");

                var shift = new Shift()
                {
                    Name = name,
                    StartDate = startDate,
                    StartTime = startTime,
                    EndTime = endTime,
                    Duration = duration
                };
                var jsonData = JsonConvert.SerializeObject(shift);

                using (HttpClient httpClient = new HttpClient())
                {
                    using(StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json"))
                    {
                        HttpResponseMessage response = await httpClient.PostAsync(apiUrl, content);

                        if(response.IsSuccessStatusCode)
                        {
                            Console.WriteLine("Shift Added. Press any button to continue.");
                            Console.ReadLine();
                        }
                        else
                        {
                            Console.WriteLine("Error: " + response.StatusCode);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
            }

        }
        public static async Task DeleteShift(int Id)
        {
            try
            {
                var apiUrl = $"https://localhost:7217/api/ShiftTimes/{Id}";

                using (HttpClient httpClient = new HttpClient())
                {
                    HttpResponseMessage response = await httpClient.DeleteAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Shift deleted. Press any button to continue.");
                        Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("Error: " + response.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex}");
            }
        }

        public static async Task PutShift(int Id, string name, string startDate, string startTime, string endTime, string duration)
        {
            try
            {
                var apiUrl = $"https://localhost:7217/api/ShiftTimes{Id}";
                var shift = new Shift()
                {
                    Id = Id,
                    Name = name,
                    StartDate = startDate,
                    StartTime = startTime,
                    EndTime = endTime,
                    Duration = duration
                };
                
                var jsonData = JsonConvert.SerializeObject(shift);

                using (HttpClient httpClient = new HttpClient())
                {
                    using (StringContent content = new StringContent(jsonData, Encoding.UTF8 , "application/json"))
                    {
                        HttpResponseMessage response = await httpClient.PutAsync(apiUrl, content);

                        if (response.IsSuccessStatusCode)
                        {
                            Console.WriteLine("Shift Updated. Press any button to continue.");
                            Console.ReadLine();
                        }
                        else
                        {
                            Console.WriteLine("Error: " + response.StatusCode);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex}");
            }
        }
    }
}
