using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftLoggerConsoleUI.Models
{
    internal class DisplayShiftDto
    {

        //public class Rootobject
        //{
        //    public Datum[] data { get; set; }
        //    public bool success { get; set; }
        //    public string message { get; set; }
        //}

        public int Id { get; set; }
        public DateTime ShiftStart { get; set; }
        public DateTime ShiftEnd { get; set; }
        public string Duration { get; set; }

    }
}
