using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftLoggerConsoleUI.Models
{
    internal class UpdateShiftDto
    {
        public DateTime ShiftStart { get; set; }
        public DateTime ShiftEnd { get; set; }
    }
}
