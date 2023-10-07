using System.Web;
using Newtonsoft.Json;
using RestSharp;
using ShiftLogger.Models;

namespace ShiftLogger
{
    public class ShiftService
    {
        public static void CheckApiIsConnected()
        {
            Console.WriteLine("Checking API is connected");
            var client = new RestClient("https://localhost:7048/");
            var request = new RestRequest("api/ShiftLogger");
            var response = client.ExecuteAsync(request);           

            if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine("\nAPI is connected: Status Code - " + response.Result.StatusCode + " press enter to go to Main Menu");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("\nAPI Error: Status Code - " + response.Result.StatusCode + " API not connected press enter to go to Main Menu");
                Console.ReadLine();
            }
        }

        public static List<Shift> GetAllShifts()
        {
            var client = new RestClient("https://localhost:7048/");
            var request = new RestRequest("api/ShiftLogger");
            var response = client.ExecuteAsync(request);
            List<Shift> shifts = new();

            if (response.Result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string rawResponse = response.Result.Content;
                var serialize = JsonConvert.DeserializeObject<List<Shift>>(rawResponse);
                shifts = serialize as List<Shift>;
                TableVisualisation.ShowTable(shifts, "Shift Logger");
                return shifts;
            }
            else
            {
                Console.WriteLine("\nAPI Error: Status Code - " + response.Result.StatusCode + " Returning to Main Menu");
            }
            return shifts;
        }
        public static void DeleteAShift()
        {
            string recordId = UserInput.GetNumberInput("\n\nPlease type the Id of the shift you want to delete.");
            recordId = Validator.CheckShiftId(recordId);
            var client = new RestClient("https://localhost:7048/");
            var request = new RestRequest($"api/ShiftLogger/{HttpUtility.UrlEncode(recordId)}", Method.Delete);
            var response = client.ExecuteAsync(request);
            if (response.Result.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                Console.WriteLine("\nDELETE request was successful.");
            }
            else
            {
                Console.WriteLine("\nDELETE request failed with status code: " + response.Result.StatusCode + " Returning to Main Menu");
            }
        }
        public static void InsertAShift()
        {
            string LastName;
            string FirstName;
            string StartTime;
            string Date;
            string EndTime;
            int Duration;

            (FirstName, LastName) = GetEmployee();
            Date = UserInput.GetShiftDate("\nPlease insert the date of shift in the format dd:mm:yy:");
            (Duration, StartTime, EndTime) = CalculateDuration();         
            var client = new RestClient("https://localhost:7048/");
            var request = new RestRequest($"api/ShiftLogger/", Method.Post);

            request.AddJsonBody(new
            {
                firstName = FirstName,
                lastName = LastName,
                date = Date,    
                shiftStartTime = StartTime,
                shiftEndTime = EndTime,
                duration = Duration.ToString()
            });
            var response = client.ExecuteAsync(request);
            if (response.Result.StatusCode == System.Net.HttpStatusCode.Created)
            {
                Console.WriteLine("\nINSERT request was successful.");
            }
            else
            {
                Console.WriteLine("\nINSERT request failed with status code: " + response.Result.StatusCode + " Returning to Main Menu");
            }
        }
        private static (string, string)GetEmployee()
        {
            string firstName = UserInput.GetEmployeeName("\nPlease insert the employee first name (do not use spaces):");
            string lastName = UserInput.GetEmployeeName("\nPlease insert the employee last name (do not use spaces):");
            return (firstName, lastName);
        }
        internal static void UpdateAShift()
        {
            var recordIdInt = UserInput.GetNumberInput("\n\nPlease type the Id of the shift you want to update.");
            string recordId = recordIdInt.ToString();
            recordId = Validator.CheckShiftId(recordId);
            if (recordId == "0")
            {
                Console.WriteLine("\nEnter any key to go back to the main menu.");
                Console.ReadLine();
                Console.Clear();
            }
            else
            {
                string LastName;
                string FirstName;
                string Date;
                string StartTime;
                string EndTime;
                int Duration;

                (FirstName, LastName) = GetEmployee();
                Date = UserInput.GetShiftDate("\nPlease insert the date of shift in the format DD:MM:YY:");
                (Duration, StartTime, EndTime) = CalculateDuration();
                var client = new RestClient("https://localhost:7048/");
                var request = new RestRequest($"api/ShiftLogger/{HttpUtility.UrlEncode(recordId)}", Method.Put);

                request.AddJsonBody(new
                {
                    id = recordId,
                    firstName = FirstName,
                    lastName = LastName,
                    date = Date,
                    shiftStartTime = StartTime,
                    shiftEndTime = EndTime,
                    duration = Duration.ToString()
                });
                var response = client.ExecuteAsync(request);
                if (response.Result.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    Console.WriteLine("\nUPDATE request was successful.");
                }
                else
                {
                    Console.WriteLine("\nUPDATE request failed with status code: " + response.Result.StatusCode + " Returning to Main Menu");
                }
            }
        }
        public static (int, string, string) CalculateDuration()
        {
            string startTime = UserInput.GetTime("\nPlease insert the start time: (24 Hour Format:HH:mm).");
            string endTime = UserInput.GetTime("\nPlease insert the end time: (24 Hour Format:HH:mm).");
            int duration = 0;
            int totalStartMinutes = 0;
            int totalEndMinutes = 0;
            TimeSpan durationStart;
            TimeSpan durationEnd;
            if (TimeSpan.TryParseExact(startTime, @"hh\:mm", null, out durationStart))
            {
                totalStartMinutes = (durationStart.Hours * 60) + durationStart.Minutes;
            }
            else
            {
                startTime = UserInput.GetTime("\nError calculating start time. Please re-insert the start time: (24 Hour Format:HH:mm).");
            }

            if (TimeSpan.TryParseExact(endTime, @"hh\:mm", null, out durationEnd))
            {
                totalEndMinutes = (durationEnd.Hours * 60) + durationEnd.Minutes;
            }
            else
            {
                endTime = UserInput.GetTime("\nError calculating end time. Please re-insert the end time: (24 Hour Format:HH:mm).");
            }

            while (((totalEndMinutes < totalStartMinutes) || (totalStartMinutes == totalEndMinutes)))
            {
                Console.WriteLine("\nInvalid duration, end time must be greater than and not equal to start time.");
                endTime = UserInput.GetTime("\nPlease insert the end time: (24 Hour Format:HH:mm).");

                if (TimeSpan.TryParseExact(startTime, @"hh\:mm", null, out durationStart))
                {
                    totalStartMinutes = (durationStart.Hours * 60) + durationStart.Minutes;
                }

                if (TimeSpan.TryParseExact(endTime, @"hh\:mm", null, out durationEnd))
                {
                    totalEndMinutes = (durationEnd.Hours * 60) + durationEnd.Minutes;
                }
            }
            duration = totalEndMinutes - totalStartMinutes;
            return (duration, startTime, endTime);
        }
    }
}
