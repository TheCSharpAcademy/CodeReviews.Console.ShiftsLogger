using Microsoft.IdentityModel.Tokens;
using ShiftLogger.API.Models;

namespace ShiftLogger.UI
{
    internal class Program
    {
        static bool endApp = false;

        static async Task Main()
        {
            await Startup();
        }

        static async Task Startup()
        {
            ShiftService.InitializeClient();
            Viewer.DisplayTitle();
            while (!endApp)
            {
                Viewer.DisplayOptionsMenu();
                string input = UserInput.GetOption();
                ProcessOption(input).Wait();
            }
            Exit();
        }

        private static async Task ProcessOption(string input)
        {
            switch (input)
            {
                case "v":
                    await ViewShifts();
                    break;
                case "a":
                    await AddShift();
                    break;
                case "d":
                    await DeleteShift();
                    break;
                case "u":
                    await UpdateShift();
                    break;
                case "0":
                    endApp = true;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        private static async Task ViewShifts()
        {
            List<Shift> shifts = await ShiftService.LoadShifts();
            if (shifts.IsNullOrEmpty())
            {
                Console.WriteLine("We could not find any shifts. Try adding some.\n");
                return;
            }
            string output = "Your shifts:\n";
            foreach (Shift shift in shifts)
            {
                output += $"Shift [{shift.Id}] {shift.Duration} ({shift.StartTime} - {shift.EndTime})\n";
            }
            Console.WriteLine(output);
        }

        private static async Task UpdateShift()
        {
            await ViewShifts();
            Console.WriteLine("Which shift would you like to update? Please type the number:");
            int id = await UserInput.GetId();
            if (id == -1) { return; }
            Console.WriteLine("When did this shift start? Use the format dd/MM/yyyy HH:mm:ss");
            DateTime startTime = UserInput.GetStartTime();
            Console.WriteLine("When did this shift end? Use the format dd/MM/yyyy HH:mm:ss");
            DateTime endTime = UserInput.GetEndTime(startTime);
            ShiftRequest shift = new()
            {
                StartTime = startTime,
                EndTime = endTime,
            };
            ShiftService.UpdateShift(id, shift).Wait();
        }

        private static async Task DeleteShift()
        {
            await ViewShifts();
            Console.WriteLine("Which shift would you like to delete? Please type the number:");
            int id = await UserInput.GetId();
            if (id == -1) { return; }
            ShiftService.DeleteShift(id).Wait();
        }

        private static async Task AddShift()
        {
            Console.WriteLine("When did this shift start? Use the format dd/MM/yyyy HH:mm:ss");
            DateTime startTime = UserInput.GetStartTime();
            Console.WriteLine("When did this shift end? Use the format dd/MM/yyyy HH:mm:ss");
            DateTime endTime = UserInput.GetEndTime(startTime);
            ShiftRequest shift = new()
            {
                StartTime = startTime,
                EndTime = endTime,
            };
            ShiftService.AddShift(shift).Wait();
        }

        private static void Exit()
        {
            Environment.Exit(0);
        }
    }
}