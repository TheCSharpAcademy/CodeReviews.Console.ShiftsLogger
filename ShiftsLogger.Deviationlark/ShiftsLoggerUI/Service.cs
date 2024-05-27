using System.Text.Json.Nodes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace ShiftsLoggerUI
{
    public class Service
    {
        public static HttpClient httpClient = new HttpClient();
        public static List<ShiftModel> SendGetRequest(int num = 0)
        {
            var client = new RestClient("http://localhost:5280/api/Shifts");
            var request = new RestRequest("");
            List<ShiftModel> shifts = new();
            try
            {
                var response = client.ExecuteAsync(request, Method.Get);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string rawResponse = response.Result.Content;
                    shifts = JsonConvert.DeserializeObject<List<ShiftModel>>(rawResponse);
                    if (num == 0) TableVisualisation.ShowShifts(shifts);
                }
                return shifts;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return shifts;
        }
        public static async Task SendPostRequest(ShiftModel shiftModel)
        {
            var json = JsonConvert.SerializeObject(shiftModel, Formatting.Indented);
            var client = new RestClient("http://localhost:5280/api/Shifts");
            var request = new RestRequest("", Method.Post);
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            try
            {
                var response = client.ExecuteAsync(request);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    Console.WriteLine($"Shift successfully entered with API response: {response.Result.StatusCode}");
                    Console.WriteLine("Press enter to go back to main menu.");
                }
                else
                {
                    Console.WriteLine($"Error. Api response: {response.Result.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static async Task SendDeleteRequest(int id)
        {
            var client = new RestClient("http://localhost:5280/api/Shifts");
            var request = new RestRequest($"/{id}", Method.Delete);
            request.AddParameter("application/json", ParameterType.RequestBody);
            try
            {
                var response = client.ExecuteAsync(request);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    Console.WriteLine($"Shift successfully deleted with API response: {response.Result.StatusCode}");
                    Console.WriteLine("Press enter to go back to main menu.");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine($"Error. Api response: {response.Result.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static async Task SendPutRequest(long id, ShiftModel shiftModel)
        {
            var json = JsonConvert.SerializeObject(shiftModel);
            var client = new RestClient("http://localhost:5280/api/");
            var request = new RestRequest($"Shifts/{id}", Method.Put);
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            File.WriteAllText("output.json", json);
            try
            {
                var response = client.ExecuteAsync(request);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    Console.WriteLine($"Shift successfully updated with API response: {response.Result.StatusCode}");
                    Console.WriteLine("Press enter to go back to main menu.");
                }
                else
                {
                    Console.WriteLine($"Error. Api response: {response.Result.StatusCode}");
                }
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}