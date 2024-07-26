using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftsLoggerUI.Utilities
{
    internal class Validation
    { 
        public bool ValidateRange(DateTime firstRange, DateTime secondRange)
        {
           // DateTime.TryParseExact(firstRangeString, filterFormat, CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime firstRange);
           // DateTime.TryParseExact(secondRangeString, filterFormat, CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime secondRange);
            return firstRange > secondRange;
        }
    }
}

