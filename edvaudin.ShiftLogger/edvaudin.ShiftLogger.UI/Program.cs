using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ShiftLogger.API.Data;
using ShiftLogger.API.Models;
using System.Configuration;

namespace ShiftLogger.UI
{
    internal class Program
    {
        static bool endApp = false;
        static void Main(string[] args)
        {
            ShiftService.InitializeClient();
            Viewer.DisplayTitle();
            while (!endApp)
            {
                Viewer.DisplayOptionsMenu();
                string input = UserInput.GetOption();
                ProcessOption(input);
            }
            Exit();
        }

        private static void ProcessOption(string input)
        {
            switch (input)
            {
                case "v":
                    ViewShifts();
                    break;
                case "a":
                    AddShift();
                    break;
                case "d":
                    DeleteShift();
                    break;
                case "u":
                    UpdateShift();
                    break;
                case "0":
                    endApp = true;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        private static async void ViewShifts()
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

        private static void UpdateShift()
        {
            throw new NotImplementedException();
        }

        private static void DeleteShift()
        {

        }

        private static void AddShift()
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
            ShiftService.AddShift(shift);
        }

        private static void Exit()
        {
            Environment.Exit(0);
        }
    }
}