using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.Controllers;
using UI.UI;
using UI.Models;

namespace UI.Services
{
    internal class ShiftService
    {
        public static async Task UpdateShift()
        {
            var updatedShift = await UserInterface.GetUpdateInput();
            await ShiftController.UpdateShift(updatedShift);
        }
        public static async Task DeleteShift()
        {
            await ShiftController.DeleteShift(await UserInterface.GetDeleteInput());
        }
        public static async Task CreateShift()
        {
            var shift = UserInterface.GetCreateInput();
            await ShiftController.CreateShift(shift);
        }
        public static async Task ViewShifts()
        {
            Console.Clear();
            List<Shift> shifts = await ShiftController.GetShifts();
            await UserInterface.ShowShifts(shifts);
        }
    }
}
