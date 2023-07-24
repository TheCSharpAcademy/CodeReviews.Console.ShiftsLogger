using Newtonsoft.Json;
using RestSharp;
using Spectre.Console;
using System.Globalization;
using System.Web;

namespace ShiftLoggerApp
{
    public class ShfitLoggerService
    {
        private static string ApiUrl = "https://localhost:7099/";
        public static void CreateShift()
        {
            string format = "dd/MM/yyyy HH:mm";

            string FullName = UserInput.GetUserFullName();

            string StartTime = UserInput.GetStartDate();

            string EndTime = UserInput.GetEndDate(StartTime);

            DateTime parsedStartTime = DateTime.ParseExact(StartTime, format, CultureInfo.InvariantCulture, DateTimeStyles.None);
            DateTime parsedEndTime = DateTime.ParseExact(EndTime, format, CultureInfo.InvariantCulture, DateTimeStyles.None);

            TimeSpan WorkedTime = parsedEndTime - parsedStartTime;

            string parsedToISO8601StartTime = parsedStartTime.ToString("o");
            string parsedToISO8601EndTime = parsedEndTime.ToString("o");

            var client = new RestClient(ApiUrl);
            var request = new RestRequest("api/Shifts", Method.Post);

            request.AddJsonBody(new
            {
                fullName = FullName,
                startTime = parsedToISO8601StartTime,
                endTime = parsedToISO8601EndTime
            });

            var response = client.ExecuteAsync(request);

            Console.WriteLine("\nEnter any key to go back to the main menu.");
            Console.ReadLine();
            Console.Clear();
        }

        public static List<Shift> ReadShifts()
        {
            var client = new RestClient(ApiUrl);
            var request = new RestRequest("api/Shifts");
            var response = client.ExecuteAsync(request);

            List<Shift> shifts = new();

            if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var rawResponse = response.Result.Content;

                var deserializedResponse = JsonConvert.DeserializeObject<List<Shift>>(rawResponse);

                shifts = deserializedResponse as List<Shift>;

                TableVisualizationEngine.ShowTable(shifts, "Your shifts");

                return shifts;
            }

            return shifts;
        }

        public static Shift ReadShift()
        {
            List<Shift> shiftsToUpdate = ReadShifts();
            Shift chosenShift = new();
            string chosenId = UserInput.GetId("REMINDER: You can go back to the main menu by typing M.\n\n Which shift do you want to read?", shiftsToUpdate);

            var client = new RestClient(ApiUrl);
            var request = new RestRequest($"api/Shifts/{HttpUtility.UrlEncode(chosenId)}");
            var response = client.ExecuteAsync(request);

            if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var rawResponse = response.Result.Content;

                var deserializedResponse = JsonConvert.DeserializeObject<Shift>(rawResponse);

                chosenShift = deserializedResponse;

                UserInterface.ShowShift(chosenShift);

                Console.WriteLine("\nEnter any key to go back to the main menu.");
                Console.ReadLine();
                Console.Clear();

                return chosenShift;
            }

            return chosenShift;
        }

        public static void UpdateShift()
        {
            string chosenId = AnsiConsole.Ask<String>("REMINDER: You can go back to the main menu by typing M.\n\n Which shift do you want to update?");

            var client = new RestClient(ApiUrl);
            var request = new RestRequest($"api/Shifts/{HttpUtility.UrlEncode(chosenId)}");

            string newFullName = UserInput.GetUserFullName();

            string newStartTimeString = UserInput.GetStartDate();
            DateTime parsedToDateTimeStartTime = DateTime.ParseExact(newStartTimeString, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None);

            string newEndTimeString = UserInput.GetEndDate(newStartTimeString);
            DateTime parsedToDateTimeEndTime = DateTime.ParseExact(newEndTimeString, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None);

            TimeSpan WorkedTime = parsedToDateTimeEndTime - parsedToDateTimeStartTime;

            string parsedToISO8601StartTime = parsedToDateTimeStartTime.ToString("o");
            string parsedToISO8601EndTime = parsedToDateTimeEndTime.ToString("o");

            request.AddBody(new
            {
                id = chosenId,
                fullName = newFullName,
                startTime = parsedToISO8601StartTime,
                endTime = parsedToISO8601EndTime,
                workedTime = WorkedTime
            });

            var response = client.ExecutePutAsync(request);

            Console.WriteLine("\nEnter any key to go back to the main menu.");
            Console.ReadLine();
            Console.Clear();
        }

        public static void DeleteShift()
        {
            List<Shift> shifts = ReadShifts();
            string chosenId = UserInput.GetId("REMINDER: You can go back to the main menu by typing M.\n\n Which shift do you want to delete?", shifts);

            var client = new RestClient(ApiUrl);
            var request = new RestRequest($"api/Shifts/{HttpUtility.UrlEncode(chosenId)}", Method.Delete);
            var response = client.ExecuteAsync(request);

            Console.WriteLine("\nEnter any key to go back to the main menu.");
            Console.ReadLine();
            Console.Clear();
        }
    }
}
