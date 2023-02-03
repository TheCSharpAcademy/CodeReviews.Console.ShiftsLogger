using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftLogger.UI
{
    internal class Validator
    {
        public static bool IsValidOption(string input)
        {
            string[] validOptions = { "v", "a", "d", "u", "0" };
            foreach (string validOption in validOptions)
            {
                if (input == validOption)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
